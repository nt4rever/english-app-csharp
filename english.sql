-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Jan 05, 2023 at 10:18 AM
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
(2, 4, 5, '2022-12-29 13:39:18', 5),
(3, 5, 5, '2022-12-30 13:52:24', 5),
(4, 2, 5, '2022-12-30 13:55:50', 5),
(5, 0, 5, '2022-12-30 14:05:42', 5),
(6, 1, 5, '2022-12-31 14:25:26', 5),
(7, 4, 5, '2022-12-31 14:54:20', 5),
(8, 4, 5, '2023-01-01 05:22:11', 6),
(9, 0, 5, '2023-01-01 08:05:49', 5),
(10, 5, 5, '2023-01-01 08:33:21', 5),
(11, 5, 5, '2023-01-01 08:35:26', 5),
(12, 9, 10, '2023-01-01 08:37:14', 5),
(13, 3, 5, '2023-01-01 08:40:51', 5),
(14, 4, 5, '2023-01-01 08:57:45', 5),
(15, 4, 5, '2023-01-01 13:38:43', 6),
(16, 2, 5, '2023-01-01 15:19:35', 5),
(18, 3, 5, '2023-01-01 16:37:37', 7),
(19, 5, 5, '2023-01-02 01:59:10', 5),
(20, 9, 10, '2023-01-02 02:00:35', 6),
(21, 5, 5, '2023-01-02 03:05:15', 5),
(22, 8, 10, '2023-01-03 10:25:23', 5),
(23, 5, 5, '2023-01-03 10:27:03', 6),
(24, 3, 5, '2023-01-04 05:37:26', 6),
(25, 2, 5, '2023-01-04 09:47:59', 5),
(28, 4, 5, '2023-01-04 12:42:49', 8),
(29, 3, 5, '2023-01-04 13:42:48', 10),
(30, 4, 5, '2023-01-05 01:52:47', 5);

-- --------------------------------------------------------

--
-- Table structure for table `quiz_questions`
--

CREATE TABLE `quiz_questions` (
  `id` int NOT NULL,
  `text` varchar(255) NOT NULL,
  `answers` text NOT NULL,
  `correct` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Dumping data for table `quiz_questions`
--

INSERT INTO `quiz_questions` (`id`, `text`, `answers`, `correct`) VALUES
(1, 'The company\'s ___ policy requires all employees to wear a uniform.', 'A. dress, B. clothing, C. attire, D. garb', 0),
(2, 'We are sorry, but the meeting room is currently ___ for another event.', 'A. reserved, B. booked, C. engaged, D. hired', 0),
(3, 'The ___ of the project was delayed due to unforeseen circumstances.', 'A. completion, B. finishing, C. end, D. termination', 0),
(4, 'The ___ at the conference was excellent.', 'A. speaker, B. orator, C. presenter, D. lecturer', 0),
(5, 'The ___ for the new product launch has been set for next month.', 'A. date, B. time, C. schedule, D. timetable', 0),
(6, 'Who are all ________ people?', 'this,those,them,that', 1),
(7, 'Claude is ________.', 'A.Frenchman,B.French,C.Frenchmen,D.French man', 1),
(8, 'I ____ a car next year.', 'A.buy,B.am buying,C.going to buy,D.bought', 1),
(9, 'They are all ________ ready for the party.', 'A.getting,B.going,C.doing,D.putting', 0),
(10, 'When do you go ________ bed?', 'A.to,B.to the,C.in,D.in the', 0),
(11, 'London is famous for _____ red buses.', 'A.it???s,B.its,C.it,D.it is', 1),
(12, 'Is there _____ milk in the fridge?', 'A.a lot,B.many,C.much,D.some', 3),
(13, 'There is a flower shop in front _____ my house.', 'A.of,B.to,C.off,D.in', 0),
(14, 'Where are _____ children? ??? They go to school.', 'A.the,B.you,C.a,D.an', 0),
(15, 'Those students are working very _____ for their next exams.', 'A.hardly,B.hard,C.harder,D.hardest', 1),
(16, 'Jane _____ as a fashion designer for ten years before becoming a famous singer.', 'A.worked,B.is working,C.works,D.will work', 0),
(19, ' Dan can _____ the drum very well', 'A.play,B.do,C.make,D.think', 0),
(20, 'My friend is ______ so she has a lot of free time.', 'A.singer,B.married,C.single,D.free', 2),
(21, 'I know somebody ________ can play the guitar.', 'A.he,B.who,C.what,D.that he', 1),
(22, ' Did you ask your father ________ some money?', 'A.of,B.after,C.on,D.for', 3),
(23, 'You look ________ in red!', 'A.most nicely,B.too nice,C.nicely,D.very nice', 3),
(24, 'We know their address, but they don???t know ________.', 'A.ours,B.their???s,C.our???s,D.our', 0),
(25, 'Can you use ________ computer?', 'A.a,B.one,C.two,D.an', 0),
(26, 'Under no circumstances ________ or exchanged.', 'A.good will be,B.goods should be,C.can good be,D.are goods being', 2),
(27, ' Frank ________ when he noticed a large packing case lying on the floor', 'A. has about to leave,B. had about to leave,C.is about to leave,D. was about to leave', 3),
(28, 'He ________ newspapers for ten years.', 'A. is selling,B.sells,C. has been selling,D.has been sold', 2),
(29, 'This luggage is quite similar to ________.', 'A.that one,B.those,C.in additional,D.that', 0),
(30, 'The doctor showed the new nurse ________ to do.', 'A.what,B.that,C.how,D.as', 0),
(31, 'Frede came to the meeting but Charles ________.', 'A.isn???t,B.hasn???t,C.didn???t,D.wasn???t', 2),
(32, 'What does he ________ for a living?', 'A.make,B.does,C.do,D.makes', 2),
(33, 'The murderer was ________ yesterday.', 'A.hanging,B.hung,C.hanged,D.hang', 1),
(34, 'Don???t let your brothers ________ the present.', 'A.to see,B.seeing,C.seen,D.see', 3),
(35, ' No, thank you, I don???t ________ sugar in tea.', 'A.take,B.put,C.eat,D.drink', 1),
(36, 'My cousin ________ bank manager.', 'A.is a,B.makes,C.is,D.he is', 0),
(37, 'She???s talking to you. Please listen to ________.', 'A.she,B.hers,C.her,D.him', 2);

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
(5, 'Tai', 'tai@gmail.com', 'vVb6JpLI1qj/gpQkFWCNbJDABSEev4XW8/T0t+zq570='),
(6, 'TanR', 'tan@gmail.com', 'vVb6JpLI1qj/gpQkFWCNbJDABSEev4XW8/T0t+zq570='),
(7, 'Test', 'test@gmail.com', 'vVb6JpLI1qj/gpQkFWCNbJDABSEev4XW8/T0t+zq570='),
(8, 'Ly', 'ly@gmail.com', 'vVb6JpLI1qj/gpQkFWCNbJDABSEev4XW8/T0t+zq570='),
(10, 'Test1', 'test1@gmail.com', 'vVb6JpLI1qj/gpQkFWCNbJDABSEev4XW8/T0t+zq570=');

-- --------------------------------------------------------

--
-- Table structure for table `vocabularies`
--

CREATE TABLE `vocabularies` (
  `id` int NOT NULL,
  `user_id` int NOT NULL,
  `name` varchar(255) NOT NULL,
  `type` varchar(255) NOT NULL,
  `meaning` varchar(255) NOT NULL,
  `note` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Dumping data for table `vocabularies`
--

INSERT INTO `vocabularies` (`id`, `user_id`, `name`, `type`, `meaning`, `note`) VALUES
(1, 5, 'percept', 'v', 'Nh??n nh???n', ''),
(6, 5, 'grammar', 'n', 'Ng??? ph??p', ''),
(7, 5, 'home', 'n', 'Nh??', ''),
(8, 5, 'Bodyguard', 'n', 'V??? s??', ''),
(9, 5, 'Judge', 'n', 'S??? ph??n x??t', ''),
(10, 5, 'Lawyer', 'n', ' Lu???t s??', ''),
(11, 5, 'Lawyer', 'n', 'Lu???t s??', ''),
(12, 5, 'Prison', 'n', 'Nh?? t??', ''),
(13, 5, 'officer', 'n', 'Nh??n vi??n v??n ph??ng', ''),
(14, 5, 'Security', 'n', 'B???o m???t', ''),
(15, 5, 'Customer', 'n', 'Kh??ch h??ng', ''),
(16, 5, 'Detective', 'n', 'Th??m t???', ''),
(17, 5, 'Dog', 'n', 'Con ch??', ''),
(18, 5, 'Cat', 'n', 'Con m??o', ''),
(19, 5, 'Chick', 'n', 'Con g?? con', ''),
(20, 5, 'Camel', 'n', 'Con l???c ????', ''),
(21, 5, 'awake', 'v', 'th???c d???y', ''),
(22, 5, 'backslide', 'v', 'T??i ph???m', ''),
(23, 5, 'befall', 'v', 'x???y ?????n', ''),
(24, 5, 'Wage', 'n', 'Ti???n l????ng', ''),
(25, 5, 'Cement', 'n', 'xi m??ng', ''),
(26, 5, 'Shopping', 'v', 'Mua s???m', ''),
(27, 5, 'Shipping', 'v', 'V???n chuy???n', ''),
(28, 5, 'promise', 'v', 'h???a', ''),
(29, 6, 'substantially', 'adv', 'v??? c??n b???n', 'v??? th???c ch???t'),
(30, 6, 'heritage', 'n', 's??? th???a k???', ''),
(31, 6, 'attain', 'v', '?????t ???????c', ''),
(32, 6, 'condensed', 'adj', '???????c r??t g???n', ''),
(33, 6, 'critically', 'adv', 'ph?? b??nh', 'quan tr???ng'),
(34, 6, 'ecology', 'n', 'sinh th??i h???c', ''),
(35, 6, 'manuscript', 'n', 'b???n th???o', ''),
(36, 6, 'obstruct', 'v', 'l??m tr??? ng???i', ''),
(37, 6, 'proliferation', 'n', 's??? gia t??ng', ''),
(38, 6, 'routinely', 'adv', 'th?????ng xuy??n', ''),
(39, 6, 'talented', 'adj', 'c?? t??i', ''),
(40, 6, 'acclaimed', 'adj', '???????c khen ng???i', ''),
(41, 6, 'anecdote', 'n', 'giai tho???i', '/??an??k??d??t/'),
(42, 8, 'table', 'n', 'cai ban', '/??te??.b??l/'),
(43, 8, 'dictionary', 'n', 't??? ??i???n', 'cuon tu dien'),
(44, 5, 'book', 'v', 'dat', ''),
(45, 5, 'book', 'n', 's??ch', '');

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
-- Indexes for table `vocabularies`
--
ALTER TABLE `vocabularies`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_user_vocab` (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `quizz_achievements`
--
ALTER TABLE `quizz_achievements`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT for table `quiz_questions`
--
ALTER TABLE `quiz_questions`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `vocabularies`
--
ALTER TABLE `vocabularies`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `quizz_achievements`
--
ALTER TABLE `quizz_achievements`
  ADD CONSTRAINT `fk_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `vocabularies`
--
ALTER TABLE `vocabularies`
  ADD CONSTRAINT `fk_user_vocab` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
