-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Erstellungszeit: 08. Jul 2020 um 23:17
-- Server-Version: 10.3.17-MariaDB
-- PHP-Version: 7.3.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `gangwars`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bans`
--

CREATE TABLE `bans` (
  `UID` int(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `HardwareIdHash` varchar(255) NOT NULL,
  `HardwareIdExHash` varchar(255) NOT NULL,
  `SocialID` int(255) NOT NULL,
  `Reason` varchar(255) NOT NULL,
  `Admin` varchar(255) NOT NULL,
  `Bantime` timestamp(6) NOT NULL DEFAULT current_timestamp(6) ON UPDATE current_timestamp(6),
  `BanCreated` timestamp(6) NOT NULL DEFAULT '0000-00-00 00:00:00.000000',
  `BanType` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `player`
--

CREATE TABLE `player` (
  `UID` int(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `SocialId` varchar(255) NOT NULL,
  `HardwareIdHash` varchar(255) NOT NULL,
  `HardwareIdExHash` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `user`
--

CREATE TABLE `user` (
  `UID` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Playtime` int(255) NOT NULL DEFAULT 0,
  `Kills` int(255) NOT NULL DEFAULT 0,
  `Deaths` int(255) NOT NULL DEFAULT 0,
  `MaxStreak` int(255) NOT NULL DEFAULT 0,
  `CStreak` int(255) NOT NULL DEFAULT 0,
  `ALevel` int(255) NOT NULL DEFAULT 0,
  `Level` int(255) NOT NULL DEFAULT 1,
  `EXP` int(255) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `bans`
--
ALTER TABLE `bans`
  ADD PRIMARY KEY (`UID`);

--
-- Indizes für die Tabelle `player`
--
ALTER TABLE `player`
  ADD PRIMARY KEY (`UID`);

--
-- Indizes für die Tabelle `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`UID`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `player`
--
ALTER TABLE `player`
  MODIFY `UID` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=102;

--
-- AUTO_INCREMENT für Tabelle `user`
--
ALTER TABLE `user`
  MODIFY `UID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=102;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
