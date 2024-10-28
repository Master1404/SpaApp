import React, { useState } from 'react';
import './HomePage.css'; // Импорт стилей для компонента
import LoginModal from './LoginModal';



function HomePage() {
    const [isModalOpen, setModalOpen] = useState(false);

    const handleLogInClick = () => {
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
    };
    return (
        <div className="home-container">
            <div className="hero-section">
                <div className="hero-content">
                    <h1>Welcome to our Spa</h1>
                    <p>Relax and enjoy our services!</p>
                    <div className="auth-buttons">
                        <button className="btn" onClick={handleLogInClick}>Log In</button>
                        <button className="btn">Log Up</button>
                    </div>
                </div>
            </div>
            <LoginModal isOpen={isModalOpen} onClose={handleCloseModal} />
        </div>
    );
}

export default HomePage;