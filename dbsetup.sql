-- CREATE TABLE profiles (
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     picture VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id)
-- )
-- CREATE TABLE keeps (
--     id INT AUTO_INCREMENT NOT NULL,
--     creatorId VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR (255) NOT NULL,
--     image VARCHAR(255) NOT NULL,
--     views INT NOT NULL,
--     shares INT NOT NULL,
--     keeps INT NOT NULL,
--     PRIMARY KEY (id),
--     FOREIGN KEY(creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE
-- )
CREATE TABLE vaults (
    id INT AUTO_INCREMENT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    title VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    isPrivate TINYINT NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY(creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE
)