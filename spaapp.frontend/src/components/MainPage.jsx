import React, { useState } from 'react';
import './MainPage.css'; // ������ ������ ��� ����������
import LoginModal from './LoginModal'; // ������ ������ ���������� ����

function MainPage() {
    const [isModalOpen, setModalOpen] = useState(false);
    const [username, setUsername] = useState(null); // �������� ����� ������������

    const handleLogInClick = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };

    const handleLoginSuccess = (user) => {
        setUsername(user); // ��������� ��� ������������ ��� �������� �����������
        setModalOpen(false); // ��������� �����
    };
    
    return (
        <div className="home-container">
            <div className="hero-section">
                <div className="hero-content">
                    <h1>Welcome to our Spa</h1>
                    <p>Relax and enjoy our services!</p>
                    {username ? (
                        <div className="welcome-message">
                            <p>Hello, {username}!</p> {/* ����������� � ������ ������������ */}
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