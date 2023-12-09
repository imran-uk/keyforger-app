CREATE TABLE `deck` (
  `DeckId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Set` int NOT NULL,
  `Houses` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`DeckId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `generated_deck` (
  `generatedId` char(36) NOT NULL,
  `generatedDate` datetime NOT NULL,
  `deckId` int DEFAULT NULL,
  PRIMARY KEY (`generatedId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4


CREATE TABLE `cards` (
  `CardId` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `House` int NOT NULL,
  `Type` int NOT NULL,
  `CardPips` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DeckId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`CardId`),
  KEY `IX_Cards_DeckId` (`DeckId`),
  CONSTRAINT `FK_Cards_Deck_DeckId` FOREIGN KEY (`DeckId`) REFERENCES `deck` (`DeckId`)
) ENGINE=InnoDB AUTO_INCREMENT=325 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `house` (
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci


-- data

-- house
insert into house set name="Brobnar";
insert into house set name="Logos";
insert into house set name="Mars";
insert into house set name="Untamed";
insert into house set name="Shadows";
insert into house set name="Saurian";
insert into house set name="StarAlliance ";
insert into house set name="Dis";
insert into house set name="Unfathomable";
insert into house set name="Sanctum";

-- user

create user 'keyforger'@'localhost' identified by 'foobar';
grant all on keyforger.* to 'keyforger'@'localhost';
# seperate one needed for the GLOBAL priv "PROCESS"
grant process on *.* to 'keyforger'@'localhost';

/*

 MySQL  localhost:3306 ssl  keyforger  SQL > show grants;
+----------------------------------------------------------+
| Grants for keyforger@%                                   |
+----------------------------------------------------------+
| GRANT PROCESS ON *.* TO `keyforger`@`%`                  |
| GRANT ALL PRIVILEGES ON `keyforger`.* TO `keyforger`@`%` |
+----------------------------------------------------------+
*/