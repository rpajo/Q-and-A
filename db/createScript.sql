CREATE SCHEMA `questionoverflow` ;

CREATE TABLE `questionoverflow`.`users` (
  `userId` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(45) NULL,
  `email` VARCHAR(200) NOT NULL,
  `password` CHAR(64) NOT NULL,
  `description` TEXT NULL,
  `memberSince` DATE NOT NULL,
  `location` VARCHAR(100) NULL,
  `answers` INT NULL DEFAULT 0,
  `questions` INT NULL DEFAULT 0,
  `reputation` INT NULL,
  PRIMARY KEY (`userId`),
  UNIQUE INDEX `userId_UNIQUE` (`userId` ASC),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC),
  UNIQUE INDEX `email_UNIQUE` (`email` ASC))
COMMENT = 'This schema holds information about all users on the site';


CREATE TABLE `questionoverflow`.`questions` (
  `questionId` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `userId` INT UNSIGNED NOT NULL,
  `title` VARCHAR(100) NOT NULL,
  `description` MEDIUMTEXT NOT NULL,
  `comments` INT ZEROFILL UNSIGNED NOT NULL DEFAULT 0,
  `rating` INT ZEROFILL NOT NULL DEFAULT 0,
  `date` DATE NOT NULL,
  `anonymous` BINARY(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`userId`),
  CONSTRAINT `userId`
    FOREIGN KEY (`userId`)
    REFERENCES `questionoverflow`.`users` (`userId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
COMMENT = 'This schema holds all the questions on the site.';


CREATE TABLE `questionoverflow`.`answers` (
  `answerId` INT NOT NULL AUTO_INCREMENT,
  `questionId` INT UNSIGNED NOT NULL,
  `description` MEDIUMTEXT NOT NULL,
  `rating` INT NULL DEFAULT 0,
  `date` DATE NOT NULL,
  PRIMARY KEY (`answerId`, `questionId`),
  UNIQUE INDEX `answerId_UNIQUE` (`answerId` ASC),
  CONSTRAINT `questionId`
    FOREIGN KEY (`questionId`)
    REFERENCES `questionoverflow`.`questions` (`questionId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE `questionoverflow`.`comments` (
  `commentId` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `questionId` INT UNSIGNED NOT NULL,
  `parentId` INT UNSIGNED NOT NULL DEFAULT 0,
  `description` TINYTEXT NOT NULL,
  `author` VARCHAR(45) NOT NULL,
  `rating` VARCHAR(45) NULL,
  `date` DATE NOT NULL,
  PRIMARY KEY (`commentId`, `questionId`),
  UNIQUE INDEX `commentId_UNIQUE` (`commentId` ASC),
  INDEX `questionId_idx` (`questionId` ASC),
  CONSTRAINT `questionIdComment`
    FOREIGN KEY (`questionId`)
    REFERENCES `questionoverflow`.`questions` (`questionId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
