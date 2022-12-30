-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Dec 29, 2022 at 09:45 PM
-- Server version: 8.0.24
-- PHP Version: 8.0.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `english`
--

-- --------------------------------------------------------

--
-- Table structure for table `quizz_achievements`
--

CREATE TABLE `quizz_achievements` (
  `id` int NOT NULL,
  `score` int NOT NULL,
  `question` int NOT NULL,
  `time` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `user_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Dumping data for table `quizz_achievements`
--

INSERT INTO `quizz_achievements` (`id`, `score`, `question`, `time`, `user_id`) VALUES
(1, 4, 5, '2022-12-29 13:16:14', 5),
(2, 4, 5, '2022-12-29 13:39:18', 5);

-- --------------------------------------------------------

--
-- Table structure for table `quiz_questions`
--

CREATE TABLE `quiz_questions` (
  `id` int NOT NULL,
  `text` varchar(255) NOT NULL,
  `answers` text NOT NULL,
  `correct_answer` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Dumping data for table `quiz_questions`
--

INSERT INTO `quiz_questions` (`id`, `text`, `answers`, `correct_answer`) VALUES
(1, 'The company\'s ___ policy requires all employees to wear a uniform.', 'A. dress, B. clothing, C. attire, D. garb', 0),
(2, 'We are sorry, but the meeting room is currently ___ for another event.', 'A. reserved, B. booked, C. engaged, D. hired', 0),
(3, 'The ___ of the project was delayed due to unforeseen circumstances.', 'A. completion, B. finishing, C. end, D. termination', 0),
(4, 'The ___ at the conference was excellent.', 'A. speaker, B. orator, C. presenter, D. lecturer', 0),
(5, 'The ___ for the new product launch has been set for next month.', 'A. date, B. time, C. schedule, D. timetable', 0);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `name`, `email`, `password`) VALUES
(5, 'Tai', 'tai@gmail.com', 'vVb6JpLI1qj/gpQkFWCNbJDABSEev4XW8/T0t+zq570=');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `quizz_achievements`
--
ALTER TABLE `quizz_achievements`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user` (`user_id`);

--
-- Indexes for table `quiz_questions`
--
ALTER TABLE `quiz_questions`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `quizz_achievements`
--
ALTER TABLE `quizz_achievements`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `quiz_questions`
--
ALTER TABLE `quiz_questions`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `quizz_achievements`
--
ALTER TABLE `quizz_achievements`
  ADD CONSTRAINT `fk_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
