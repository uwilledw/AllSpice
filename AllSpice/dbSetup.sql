CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8mb4 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title VARCHAR(50) NOT NULL,
        instructions VARCHAR(2047) NOT NULL,
        category VARCHAR(50) NOT NULL,
        img VARCHAR(255) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name VARCHAR(50) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

INSERT INTO ingredients();

DROP TABLE recipes;

INSERT INTO
    recipes (
        title,
        instructions,
        category,
        img,
        creatorId
    )
VALUES (
        'Frito Mole Pie',
        'Lorem ipsum dolor sit amet consectetur adipisicing elit. At porro placeat magnam molestiae est? Veritatis et quo iste officiis quidem? Voluptate pariatur labore ex ut laborum maiores id sapiente repudiandae. Lorem ipsum, dolor sit amet consectetur adipisicing elit. Laudantium modi quia pariatur? Officia inventore temporibus atque, optio ipsum illo iste corporis et minima vitae possimus laboriosam ea. Aut, placeat harum?',
        'misc',
        'https://bing.com/th?id=OSK.d2f6fc51b2cd9aa681511c506b133200',
        '64498d5b793406b9a25ca11c'
    );

CREATE TABLE
    IF NOT EXISTS favorites(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        accountId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY(accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        UNIQUE(recipeId, accountId)
    ) default charset utf8mb4 COMMENT '';