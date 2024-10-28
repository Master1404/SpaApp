import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './LoginModal.css'; // Создайте CSS файл для стилей модального окна

const LoginModal = ({ isOpen, onClose, onLoginSuccess }) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(''); // Для хранения ошибок
    const navigate = useNavigate(); // Инициализируем useNavigate

    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log('username', { username });

        // Простая проверка на пустые поля
        if (!username || !password) {
            setError('Username and password are required!');
            return;
        }

        try {
            const response = await axios.post('http://localhost:5268/api/v1/Auth/login', {
                username,
                password,
            }, {
                headers: {
                    'Accept': '*/*',
                    'Content-Type': 'application/json',
                },
            });

            // Если успешный вход, сохраняем токен
            localStorage.setItem('token', response.data.token); // Сохранение токена в localStorage
            onLoginSuccess(username);
            navigate('/main'); // Перенаправляем на главную страницу
            onClose(); // Закрыть модал после успешного входа

        } catch (error) {
            // Обработка ошибок
            if (error.response) {
                setError('Invalid username or password');
            } else {
                setError('An error occurred. Please try again.');
            }
            console.error('Error:', error);
        }
    };

    if (!isOpen) return null;

    return (
        <div className="modal-overlay" onClick={onClose}>
            <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                <h2>Log In</h2>
                {error && <p className="error-message">{error}</p>}
                <form onSubmit={handleSubmit}>
                    <input
                        type="text"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                    <button type="submit">Submit</button>
                    <button type="button" onClick={onClose}>Close</button>
                </form>
            </div>
        </div>
    );
};

export default LoginModal;