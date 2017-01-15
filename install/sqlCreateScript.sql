-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: questionoverflow
-- ------------------------------------------------------
-- Server version	5.7.16-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `answers`
--

DROP TABLE IF EXISTS `answers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `answers` (
  `answerId` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `questionId` int(10) unsigned NOT NULL,
  `userId` int(10) unsigned NOT NULL,
  `author` varchar(45) NOT NULL,
  `description` mediumtext NOT NULL,
  `rating` int(11) DEFAULT '0',
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `solved` int(10) unsigned DEFAULT '0',
  PRIMARY KEY (`answerId`,`questionId`,`userId`),
  UNIQUE KEY `answerId_UNIQUE` (`answerId`),
  KEY `userId_idx` (`userId`),
  KEY `questionIdAnswer` (`questionId`),
  CONSTRAINT `questionIdAnswer` FOREIGN KEY (`questionId`) REFERENCES `questions` (`questionId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `userIdAnswer` FOREIGN KEY (`userId`) REFERENCES `users` (`userId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `answers`
--

LOCK TABLES `answers` WRITE;
/*!40000 ALTER TABLE `answers` DISABLE KEYS */;
INSERT INTO `answers` VALUES (1,1,1,'rpajo','The implementation could be like that\n\npublic static String sha256_hash(String value) {\n  StringBuilder Sb = new StringBuilder();\n\n  using (SHA256 hash = SHA256Managed.Create()) {\n    Encoding enc = Encoding.UTF8;\n    Byte[] result = hash.ComputeHash(enc.GetBytes(value));\n\n    foreach (Byte b in result)\n      Sb.Append(b.ToString(\"x2\"));\n  }\n\n  return Sb.ToString();\n}\nEdit: LINQ implementation which is more concise, but, probably, less readable:\n\npublic static String sha256_hash(String value) {\n  using (SHA256 hash = SHA256Managed.Create()) {\n    return String.Concat(hash\n      .ComputeHash(Encoding.UTF8.GetBytes(value))\n      .Select(item => item.ToString(\"x2\")));\n  }\n} ',32,'2016-06-13 00:00:00',1),(2,2,1,'rpajo','Updating an entry:\n\nA simple increment should do the trick.\n\nUPDATE mytable \n  SET logins = logins + 1 \n  WHERE id = 12\nInsert new row, or Update if already present:\n\nIf you would like to update a previously existing row, or insert it if it doesn\'t already exist, you can use the REPLACE syntax or the INSERT...ON DUPLICATE KEY UPDATE option (As Rob Van Dam demonstrated in his answer).\n\nInserting a new entry:\n\nOr perhaps you\'re looking for something like INSERT...MAX(logins)+1? Essentially you\'d run a query much like the following - perhaps a bit more complex depending on your specific needs:\n\nINSERT into mytable (logins) \n  SELECT max(logins) + 1 \n  FROM mytable',164,'2016-06-10 16:41:00',0),(3,2,5,'user5','If you can safely make (firstName, lastName) the PRIMARY KEY or at least put a UNIQUE key on them, then you could do this:\n\nINSERT INTO logins (firstName, lastName, logins) VALUES (\'Steve\', \'Smith\', 1)\nON DUPLICATE KEY UPDATE logins = logins + 1;\nIf you can\'t do that, then you\'d have to fetch whatever that primary key is first, so I don\'t think you could achieve what you want in one query.',53,'2016-06-10 17:19:00',0),(4,2,5,'user5','You didn\'t say what you\'re trying to do, but you hinted at it well enough in the comments to the other answer. I think you\'re probably looking for an auto increment column\n\ncreate table logins (userid int auto_increment primary key, \n  username varchar(30), password varchar(30));\nthen no special code is needed on insert. Just\n\ninsert into logins (username, password) values (\'user\',\'pass\');\nThe MySQL API has functions to tell you what userid was created when you execute this statement in client code.',-30,'2016-06-10 17:06:00',0),(5,3,1,'rpajo','That is certainly an interesting question!\n\nFirst, to clarify definitions:\n\nTo be considered venomous the toxic substance must be produced in specialized glands or tissue. Often these are associated with some delivery apparatus (fangs, stinger, etc.), but not necessarily.\n\nTo be poisonous, the toxins must be produced in non-specialized tissues and are only toxic after ingestion.\n\nInterestingly, many venoms are not poisonous if ingested.[1]\n\nI know of at least three species that produce both poison and venom. One is a snake (although not a rattlesnake, which are, in fact, edible): Rhabdophis tigrinus, which accumulates toxins in its tissues, but also delivers venom via fangs.[2] The other two are frogs: Corythomantis greeningi and Aparasphenodon brunoi, which have spines on their snout that they use to deliver the venom.[3]',57,'2016-12-20 13:42:11',0),(6,5,1,'rpajo','Well, it is kinda explained on The Daily Beast,\n\nIn the book, Ellis Boyd \"Red\" Redding was a ginger-haired, middle-aged Mick. Harrison Ford, Clint Eastwood and Paul Newman were all considered for the role that went to Morgan Freeman. Darabont alluded to the unusual casting choice by having Red jokingly reply to Andy’s inquiry about his nickname with the line, \"Maybe it\'s because I\'m Irish.\" Happily, they opted to not follow the quote with audio of a studio audience laughing.\nAnyway, it was derived from his actual surname of Ellis Boyd Redding, or \'Red\' for short, as revealed in his first parole hearing in the film.\n\nBorrowed from comments:\n\nThe physical characteristic of red hair is most common in the Irish. And for non-native speakers, \"ginger-haired\" means \"red-haired\".\n\nCredits to @G&C, \"red-haired\" is used to describe orange hair colours because orange is (relatively speaking) the newest colour name in the 7-colour spectrum (besides indigo), so the term \"red-hair\" literally precedes the use of \"orange\" as a colour.',85,'2016-12-20 14:34:30',1),(7,4,45,'apiTest','This is a test answer',2,'2016-12-28 12:01:27',0),(8,4,45,'apiTest','This is a test answer 2',-4,'2016-12-28 12:03:56',0),(9,4,45,'apiTest','This is a test answer 3',1,'2016-12-28 12:04:51',0),(10,4,45,'apiTest','This is a test answer 3',11,'2016-12-28 12:05:22',0),(11,4,45,'apiTest','This is a test answer 4',2,'2016-12-28 13:02:56',0),(13,1,45,'apiTest','This is a test answer',1,'2016-12-29 10:20:39',0),(14,6,45,'apiTest','Bla bla bla Yes ',-1,'2016-12-29 23:59:00',0),(15,4,52,'EfTestt','API EF test',11,'0001-01-01 00:00:00',0),(16,4,52,'','API EF test2',-12,'2017-01-11 22:57:51',0),(18,7,53,'passHashTest','I fell you',0,'2017-01-15 01:03:03',0);
/*!40000 ALTER TABLE `answers` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `questionoverflow`.`answers_AFTER_INSERT` AFTER INSERT ON `answers` FOR EACH ROW
BEGIN        
	UPDATE users SET `answers`= answers + 1 WHERE `userId`= new.userId;
    UPDATE questions SET answers = answers + 1 WHERE questionId = new.questionId;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `questionoverflow`.`answers_AFTER_UPDATE` AFTER UPDATE ON `answers` FOR EACH ROW
BEGIN
	if new.rating <> old.rating then        
		UPDATE `questionoverflow`.`users` SET `reputation`= reputation + (new.rating-old.rating) WHERE `userId`= new.userId;
	end if;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `questionoverflow`.`answers_BEFORE_DELETE` BEFORE DELETE ON `answers` FOR EACH ROW
BEGIN
	delete from comments
		where comments.questionId = old.questionId;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `comments`
--

DROP TABLE IF EXISTS `comments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `comments` (
  `commentId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `questionId` int(10) unsigned NOT NULL,
  `userId` int(10) unsigned NOT NULL,
  `parentId` int(10) unsigned NOT NULL DEFAULT '0',
  `description` tinytext NOT NULL,
  `author` varchar(45) NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`commentId`,`questionId`,`userId`),
  UNIQUE KEY `commentId_UNIQUE` (`commentId`),
  KEY `questionId_idx` (`questionId`),
  KEY `userId_idx` (`userId`),
  CONSTRAINT `questionIdComment` FOREIGN KEY (`questionId`) REFERENCES `questions` (`questionId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `userIdComment` FOREIGN KEY (`userId`) REFERENCES `users` (`userId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comments`
--

LOCK TABLES `comments` WRITE;
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
INSERT INTO `comments` VALUES (1,1,5,0,'Given that the SHA family of algortihms are cryptographic hash functions, I can\'t see how you possibly hope to encrypt with it. Get to grips with the difference between encryption and hashing, then come back and ask a real question','user5','2016-01-13 11:24:00'),(2,1,45,0,'Hashes work on bytes, not strings. So you first need to choose an encoding that transforms the string into bytes. I recommend UTF-8 for thi','apiTest','2016-01-13 11:55:11'),(3,1,45,1,'I can confirm this works','apiTest','2016-01-13 12:00:12'),(4,2,5,2,'Also, be sure to add your WHERE clause as appropriate to your application','apiTest','2016-06-11 10:12:11'),(5,2,1,2,'Oh really!! I wasn\'t aware you could do that!!! So would this work? INSERT IGNORE mytable (firstName, lastName, logins) VALUES (John, Smith, logins = logins + 1) ','user5','2016-06-11 16:55:06'),(6,5,45,0,'The physical characteristic of red hair is most common in the Irish.','apiTest','2016-12-23 12:13:11'),(7,4,45,0,'This is a test comment','apiTest','2016-12-28 12:50:05'),(8,4,45,7,'This is a test comment 2','apiTest','2016-12-28 12:51:18'),(11,4,45,10,'This is a test comment 3','apiTest','2016-12-28 12:59:08'),(16,4,45,0,'This is a test comment 2','apiTest','2016-12-29 10:28:52'),(17,4,45,7,'This is a test comment 3','apiTest','2016-12-29 10:28:58'),(20,4,52,0,'EF comment post test','EntityFW','2017-01-11 23:11:21'),(22,7,53,0,':/','passHashTest','2017-01-15 01:03:47'),(23,7,53,18,':Pdsdcsd','passHashTest','2017-01-15 01:03:52');
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `questions` (
  `questionId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `userId` int(10) unsigned NOT NULL,
  `author` varchar(45) NOT NULL,
  `title` varchar(100) NOT NULL,
  `description` mediumtext NOT NULL,
  `answers` int(10) unsigned NOT NULL DEFAULT '0',
  `rating` int(11) NOT NULL DEFAULT '0',
  `date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `anonymous` int(10) DEFAULT '0',
  PRIMARY KEY (`questionId`,`userId`),
  KEY `userId_idx` (`userId`),
  CONSTRAINT `userIdQuestions` FOREIGN KEY (`userId`) REFERENCES `users` (`userId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='This schema holds all the questions on the site.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questions`
--

LOCK TABLES `questions` WRITE;
/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` VALUES (1,1,'rpajo','Obtain SHA-256 string of a string','I have some string and I want to hash it with the SHA-256 hash function using C#. I want something like this:\n string hashString = sha256_hash(\"samplestring\");\nIs there something built into the framework to do this?',3,17,'2016-12-19 00:00:00',0),(2,1,'rpajo','Increment a database field by 1','With MySQL, if I have a field, of say logins, how would I go about updating that field by 1 within a sql command?\n\nI\'m trying to create an INSERT query, that creates firstName, lastName and logins. However if the combination of firstName and lastName already exists, increment the logins by 1.',3,232,'2016-12-19 00:00:00',0),(3,1,'rpajo','Are there any animals that are both poisonous and venomous?','From my layman understanding, animals that inject venom into the bloodstream by biting or poking are venomous. And ones that harm you when you eat them are poisonous.\n\nAre there any animals (or plants) that fit both descriptions?\n\nI\'m guessing eating a venomous rattlesnake will give you an upset stomach but not cause enough damage to be classified as poisonous. And I\'m pretty sure poisonous tree frogs don\'t bite into their prey and inject them with anything.',1,132,'2016-11-17 00:00:00',0),(4,1,'rpajo','What is the term for drawing wireframes which are drawn before drawing the actual object?','I wish to learn how to visualize and draw wireframes like that for any object.\nI searched Google with term \"drawing wireframes\" but didn\'t get what I was looking for.\n\nWhat term should I search in Google to learn how to visualize and draw the kind of wireframe shown in that link?',8,2,'2016-05-10 00:00:00',0),(5,1,'rpajo','Why do people call Morgan Freeman\'s character Red?','In the movie The Shawshank Redemption, when Andy first comes to Red (for smuggling a rock hammer) they have this dialogue:\n\nAndy: Thank you... Mr. er...?\n\nRed: Red. The name\'s Red.\n\nAndy: Red? Why do they call you that?\n\nRed: Maybe it\'s because I\'m Irish.\nWhy is an Irish background supposed to give him the name \"Red\"?\n\nI might be missing something since I\'m not native to England or Ireland.',1,1,'1994-08-24 00:00:00',1),(6,46,'apiTest1','Api Test Question #1','This is a random description of a test question\n\nI can\'t wait for \"this\" to be over\n\n\n:)\n\nBla bla ',1,6,'2016-12-29 00:00:00',1),(7,53,'passHashTest','Test Question','I\'m very tired...help me please',1,1,'2017-01-15 01:00:43',0);
/*!40000 ALTER TABLE `questions` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `questionoverflow`.`questions_AFTER_INSERT` AFTER INSERT ON `questions` FOR EACH ROW
BEGIN
	UPDATE users SET `questions`= questions + 1 WHERE `userId`= new.userId;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `questionoverflow`.`questions_AFTER_UPDATE` AFTER UPDATE ON `questions` FOR EACH ROW
BEGIN
	if new.rating <> old.rating then
		UPDATE `questionoverflow`.`users` SET `reputation`= reputation + (new.rating-old.rating) WHERE `userId`= new.userId;
	end if;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `questionoverflow`.`questions_BEFORE_DELETE` BEFORE DELETE ON `questions` FOR EACH ROW
BEGIN
	delete from answers
		where answers.questionId = old.questionId;
	delete from comments
		where comments.questionId = old.questionId;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `questionoverflow`.`questions_AFTER_DELETE` AFTER DELETE ON `questions` FOR EACH ROW
BEGIN
	UPDATE users SET `questions`= questions + -1 WHERE `userId`= old.userId;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `userId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(45) DEFAULT NULL,
  `email` varchar(200) NOT NULL,
  `password` varchar(256) NOT NULL,
  `description` text,
  `memberSince` date NOT NULL,
  `location` varchar(100) DEFAULT NULL,
  `answers` int(11) DEFAULT '0',
  `questions` int(11) DEFAULT '0',
  `reputation` int(11) DEFAULT '0',
  PRIMARY KEY (`userId`),
  UNIQUE KEY `userId_UNIQUE` (`userId`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8 COMMENT='This schema holds information about all users on the site';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'rpajo','rok.pajnic@gmail.com','passwd','asdmsakdmsafafdafmadskaldk cadsl','2011-01-02','Ribnica, Slovenija',0,3,9),(5,'user5','apiTest@gmail.com','password','i am a giant douche','2016-12-17','Ribnica, SLovenija',0,0,1),(45,'apiTest','aa','pass','This is a random user description via api\n\nLorem ipsum nekaj nekaj\n \n_:P','2016-12-18','Ribnica, Slovenija',9,3,17),(46,'apiTest1','api@a.com','pass','','2016-12-29','',0,1,6),(48,'RandomUser','random','pass','asdmsakdmsafafdafmadskaldk cadsl','2016-12-29','Ribnica, Slovenija',0,0,0),(49,'apiTest3','a@3.com','pass','','2016-12-29','',0,0,0),(50,'efTest','apasdiTest@gmaisadl.com','password',NULL,'2017-01-11',NULL,0,0,0),(52,'efTestt','apasdiTest@gmaisadl.comm','password',NULL,'2017-01-11',NULL,2,0,0),(53,'passHashTest','pass@hash.com','sha1:64000:18:BO6ho7R5Dp8jUOsWB2k2qXnXdVN4Ajxi:ribGen6Cl22SdkNegMVmhFBK','Something about me ','2017-01-13','Slovenija',2,1,1),(54,'ADMIN','admin@root.com','sha1:64000:18:z+wpzkc7ikCPkahfu+rgapoh3dl5CgTf:NtxCuUrbkG5QRD5ZKTOiPOf1',NULL,'2017-01-15',NULL,0,0,0),(61,'a','a@a.com','sha1:64000:18:0gFIguxkAEsfgqBLUpNqxUwjmoU9JzSf:WBgQdEf6owQMq7pU5NPgHV3e',NULL,'2017-01-15',NULL,0,0,0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-01-15 21:58:44
