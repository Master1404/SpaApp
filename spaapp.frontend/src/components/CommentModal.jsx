import React, { useState, useEffect, useRef } from 'react';
import './CommentModal.css';

function CommentModal({ isOpen, onClose, onSubmit, username/*, initialUsername */}) {
   // const [username, setUsername] = useState(initialUsername || '');
    const [email, setEmail] = useState('');
    const [homePage, setHomePage] = useState('');
    const [text, setText] = useState('');
    const [captcha, setCaptcha] = useState('');
    const [captchaInput, setCaptchaInput] = useState('');
    const captchaCanvasRef = useRef(null);
    // Функция для генерации случайного CAPTCHA
    const generateCaptcha = () => {
        const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        let captcha = '';
        for (let i = 0; i < 6; i++) {
            captcha += chars.charAt(Math.floor(Math.random() * chars.length));
        }
        setCaptcha(captcha);
    };

    // Функция для рисования CAPTCHA с искажениями
    const drawCaptcha = () => {
        const canvas = captchaCanvasRef.current;
        const ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height); // Очистка canvas
        ctx.font = '20px Arial';
        ctx.fillStyle = '#000';

        // Отображение текста CAPTCHA со случайным смещением
        for (let i = 0; i < captcha.length; i++) {
            ctx.fillStyle = getRandomColor(); // Случайный цвет для каждой буквы
            const x = 10 + i * 15;
            const y = 20 + Math.floor(Math.random() * 8) - 4; // Случайное вертикальное смещение
            ctx.fillText(captcha[i], x, y);
        }

        // Добавление случайных линий для усложнения
        for (let i = 0; i < 5; i++) {
            ctx.strokeStyle = getRandomColor();
            ctx.beginPath();
            ctx.moveTo(Math.random() * canvas.width, Math.random() * canvas.height);
            ctx.lineTo(Math.random() * canvas.width, Math.random() * canvas.height);
            ctx.stroke();
        }

        // Добавление случайных точек для создания шума
        for (let i = 0; i < 30; i++) {
            ctx.fillStyle = getRandomColor();
            ctx.beginPath();
            ctx.arc(Math.random() * canvas.width, Math.random() * canvas.height, 1, 0, 2 * Math.PI);
            ctx.fill();
        }
    };

    // Генерация случайного цвета для элементов CAPTCHA
    const getRandomColor = () => {
        const letters = '0123456789ABCDEF';
        let color = '#';
        for (let i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    };

    useEffect(() => {
        if (isOpen) {
            generateCaptcha();
        }
    }, [isOpen]);

    useEffect(() => {
        if (captcha) {
            drawCaptcha();
        }
    }, [captcha]);

    const handleSubmit = (e) => {
        e.preventDefault();
        if (captchaInput !== captcha) {
            alert('CAPTCHA does not match!');
            return;
        }

        console.log('username = ' + username);
        const commentData = {
            userName: username,
            email: email,
            homePage: homePage,
            text: text,
            createdAt: new Date().toISOString(),
        };
        onSubmit(commentData);
        setCaptchaInput('');
        generateCaptcha(); // Сброс и генерация нового CAPTCHA
    };

    if (!isOpen) return null;

    return (
        <div className="comment-modal-overlay" onClick={onClose}>
            <div className="comment-modal-content" onClick={(e) => e.stopPropagation()}>
                <h2>Add Comment</h2>
                <form onSubmit={handleSubmit}>
                    <div>
                        <label>Username:</label>
                        <input
                            type="text"
                            value={username}
                            readOnly // Username is read-only and cannot be modified
                        />
                    </div>
                    <div>
                        <label>Email:</label>
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                    </div>
                    <div>
                        <label>Home Page:</label>
                        <input
                            type="url"
                            value={homePage}
                            onChange={(e) => setHomePage(e.target.value)}
                            placeholder="https://example.com"
                        />
                    </div>
                    <div>
                        <label>Comment:</label>
                        <textarea
                            value={text}
                            onChange={(e) => setText(e.target.value)}
                            required
                        />
                    </div>
                    <div>
                        <label>CAPTCHA:</label>
                        <canvas ref={captchaCanvasRef} width="100" height="30"></canvas>
                        <input
                            type="text"
                            value={captchaInput}
                            onChange={(e) => setCaptchaInput(e.target.value)}
                            required
                            placeholder="Enter CAPTCHA"
                        />
                    </div>
                    <div className="comment-modal-buttons">
                        <button type="submit">Create</button>
                        <button type="button" onClick={onClose}>Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default CommentModal;