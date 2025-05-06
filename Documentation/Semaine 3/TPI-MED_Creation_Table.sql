-- Réinitialisation des tables (ordre inverse des dépendances)
DROP TABLE IF EXISTS events_have_sessions_type;
DROP TABLE IF EXISTS events;
DROP TABLE IF EXISTS interviews;
DROP TABLE IF EXISTS sessions_types;
DROP TABLE IF EXISTS interviews_types;
DROP TABLE IF EXISTS users;

-- -----------------------------------------------------
-- Table `users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `Mail` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `Salt` VARCHAR(45) NOT NULL,
  `CreationDate` DATETIME NOT NULL,
  `isValid` TINYINT NOT NULL,
  `token` VARCHAR(45) NULL,
  `2FACode` VARCHAR(45) NULL,
  `2FACodeDate` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `UniqueUser` (`id` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `interviews_types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `interviews_types` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `UniqueInterviewType` (`id` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `interviews`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `interviews` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `time` DATETIME NOT NULL,
  `interviews_types_id` INT NOT NULL,
  `addictive_behaviors` TINYINT NULL,
  `critical_incident` TINYINT NULL,
  `student_conflict` TINYINT NULL,
  `incivility_violence` TINYINT NULL,
  `grief` TINYINT NULL,
  `unhappiness` TINYINT NULL,
  `learning_difficulties` TINYINT NULL,
  `career_guidance_issues` TINYINT NULL,
  `family_difficulties` TINYINT NULL,
  `stress` TINYINT NULL,
  `financial_difficulties` TINYINT NULL,
  `suspected_abuse` TINYINT NULL,
  `discrimination` TINYINT NULL,
  `difficulties_tensions_with_a_teacher` TINYINT NULL,
  `harassment_intimidation` TINYINT NULL,
  `gender_sexual_orientation` TINYINT NULL,
  `other` TINYINT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Interview_Interview_Type1_idx` (`interviews_types_id` ASC) VISIBLE,
  UNIQUE INDEX `UniqueInterview` (`id` ASC) VISIBLE,
  CONSTRAINT `fk_Interview_Interview_Type1`
    FOREIGN KEY (`interviews_types_id`)
    REFERENCES `interviews_types` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `events`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `events` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `date` DATETIME NOT NULL,
  `subject` VARCHAR(45) NOT NULL,
  `person` VARCHAR(45) NOT NULL,
  `adminTime` INT(3) NOT NULL,
  `users_id` INT NOT NULL,
  `interviews_id` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Event_User_idx` (`users_id` ASC) VISIBLE,
  INDEX `fk_Event_Interview1_idx` (`interviews_id` ASC) VISIBLE,
  UNIQUE INDEX `UniqueEvent` (`id` ASC) VISIBLE,
  CONSTRAINT `fk_Event_User`
    FOREIGN KEY (`users_id`)
    REFERENCES `users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Event_Interview1`
    FOREIGN KEY (`interviews_id`)
    REFERENCES `interviews` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `sessions_types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sessions_types` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `UniqueSessionType` (`id` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `events_have_sessions_type`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `events_have_sessions_type` (
  `id` VARCHAR(45) NOT NULL,
  `sessions_types_id` INT NOT NULL,
  `events_id` INT NOT NULL,
  `time` INT(3) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Session_Type_has_Event_Event1_idx` (`events_id` ASC) VISIBLE,
  INDEX `fk_Session_Type_has_Event_Session_Type1_idx` (`sessions_types_id` ASC) VISIBLE,
  UNIQUE INDEX `UniqueEventHaveSession` (`id` ASC) VISIBLE,
  CONSTRAINT `fk_Session_Type_has_Event_Session_Type1`
    FOREIGN KEY (`sessions_types_id`)
    REFERENCES `sessions_types` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Session_Type_has_Event_Event1`
    FOREIGN KEY (`events_id`)
    REFERENCES `events` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;