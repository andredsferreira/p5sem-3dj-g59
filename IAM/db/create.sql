CREATE TABLE users (
    username VARCHAR(255) PRIMARY KEY,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    phone VARCHAR(255),
    role VARCHAR(255) NOT NULL,
    since TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);