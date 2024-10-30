import React, { useState, useEffect } from 'react';
import './MainPage.css';
import LoginModal from './LoginModal';
import CommentModal from './CommentModal';
import axios from 'axios';

function MainPage() {
    const [isModalOpen, setModalOpen] = useState(false);
    const [isCommentModalOpen, setCommentModalOpen] = useState(false);
    const [username, setUsername] = useState(null);
    const [comments, setComments] = useState([]);
    const [replyToCommentId, setReplyToCommentId] = useState(null); // ID комментария, на который отвечаем

    const fetchComments = async () => {
        const token = localStorage.getItem('token'); // Получаем токен
        try {
            const response = await axios.get('http://localhost:5268/api/v1/Comment/list', {
                headers: {
                    'Authorization': `Bearer ${token}`, // Отправляем токен в заголовках
                },
            });
            if (response.status === 200) {
                setComments(response.data);
            }
        } catch (error) {
            console.error('Ошибка при получении комментариев:', error);
        }
    };

    const handleLogInClick = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleLoginSuccess = (user) => {
        setUsername(user);
        setModalOpen(false);
        fetchComments(); // Получаем комментарии после успешного входа
    };

    const handleLogout = () => {
        setUsername(null); // Очищаем состояние пользователя
        localStorage.removeItem('token'); // Удаляем токен из localStorage
        setComments([]); // Очистка комментариев (опционально)
    };

    const handleOpenCommentModal = (commentId = null) => {
        setReplyToCommentId(commentId);
        setCommentModalOpen(true);
    };

    const handleCloseCommentModal = () => {
        setCommentModalOpen(false);
        setReplyToCommentId(null); // Сброс ID комментария при закрытии
    };

    const handleCommentSubmit = async (commentData) => {
        const token = localStorage.getItem('token');

        try {
            const response = await axios.post('http://localhost:5268/api/v1/Comment', commentData, {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                },
            });

            if (response.status === 200 || response.status === 201) {
                const newComment = response.data;

                // Если это ответ на комментарий, изменяем текст
                if (replyToCommentId) {
                    newComment.text = `Reply to user ${comments.find(comment => comment.id === replyToCommentId).userName}: ${newComment.text}`;
                }

                // Добавляем новый комментарий
                setComments([...comments, newComment]);
                setCommentModalOpen(false);
            } else {
                console.error('Ошибка при создании комментария', response);
            }
        } catch (error) {
            console.error('Ошибка сети:', error);
        }
    };

    return (
        <div className="home-container">
            <div className="hero-section">
                <div className="hero-content">
                    <h1>Welcome to our Spa</h1>
                    <p>Relax and enjoy our services!</p>
                    {username ? (
                        <div className="welcome-message">
                            <p>Hello, {username}!</p>
                            <button className="btn" onClick={() => handleOpenCommentModal()}>Add Comment</button>
                            <button className="btn" onClick={handleLogout}>Logout</button> {/* Кнопка выхода */}
                        </div>
                    ) : (
                        <div className="auth-buttons">
                            <button className="btn" onClick={handleLogInClick}>Log In</button>
                            <button className="btn">Sign Up</button>
                        </div>
                    )}
                </div>

                <div className="comments-section">
                    <h2>Comments</h2>
                    <ul className="comments-list">
                        {comments.map((comment) => (
                            <li key={comment.id} className="comment-item">
                                <div className="comment-header">
                                    <strong>{comment.userName}</strong> ({comment.email})
                                    <span className="comment-date">{new Date(comment.createdAt).toLocaleString()}</span>
                                    <button className="btn" onClick={() => handleOpenCommentModal(comment.id)}>Reply</button>
                                </div>
                                <div className="comment-text">{comment.text}</div>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>

            <LoginModal
                isOpen={isModalOpen}
                onClose={handleCloseModal}
                onLoginSuccess={handleLoginSuccess}
            />
            <CommentModal
                isOpen={isCommentModalOpen}
                onClose={handleCloseCommentModal}
                onSubmit={handleCommentSubmit}
                username={username}
            />
        </div>
    );
}

export default MainPage;