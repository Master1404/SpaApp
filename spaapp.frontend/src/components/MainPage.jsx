import React, { useState } from 'react';
import './MainPage.css'; // Импорт стилей для компонента
import LoginModal from './LoginModal'; // Импорт вашего модального окна

function MainPage() {
    const [isModalOpen, setModalOpen] = useState(false);
    const [username, setUsername] = useState(null); // Хранение имени пользователя

    const handleLogInClick = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleLoginSuccess = (user) => {
        setUsername(user); // Обновляем имя пользователя при успешной авторизации
        setModalOpen(false); // Закрываем модал
    };
    
    return (
        <div className="home-container">
            <div className="hero-section">
                <div className="hero-content">
                    <h1>Welcome to our Spa</h1>
                    <p>Relax and enjoy our services!</p>
                    {username ? (
                        <div className="welcome-message">
                            <p>Hello, {username}!</p> {/* Приветствие с именем пользователя */}
                        </div>
                    ) : (
                        <div className="auth-buttons">
                            <button className="btn" onClick={handleLogInClick}>Log In</button>
                            <button className="btn">Log Up</button>
                        </div>
                    )}
                </div>
            </div>
            <LoginModal
                isOpen={isModalOpen}
                onClose={handleCloseModal}
                onLoginSuccess={handleLoginSuccess} 
            />
        </div>
    );
}

export default MainPage;