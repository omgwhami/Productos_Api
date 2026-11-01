CREATE DATABASE IF NOT EXISTS tienda;
USE tienda;

CREATE TABLE IF NOT EXISTS productos (
    id          INT AUTO_INCREMENT PRIMARY KEY,
    nombre      VARCHAR(100)   NOT NULL,
    descripcion VARCHAR(255)   NULL,
    precio      DECIMAL(10,2)  NOT NULL,
    stock       INT            NOT NULL DEFAULT 0
);

INSERT INTO productos (nombre, descripcion, precio, stock) VALUES
('Mouse inalámbrico', 'Mouse USB 2.4GHz', 199.90, 25),
('Teclado mecánico', 'Switches azules', 849.00, 10),
('Monitor 24"', 'Full HD IPS', 2499.00, 5);
