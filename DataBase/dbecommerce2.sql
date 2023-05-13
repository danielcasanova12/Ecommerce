-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 13/05/2023 às 21:04
-- Versão do servidor: 10.4.28-MariaDB
-- Versão do PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `dbecommerce2`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `tb_itempedido`
--

CREATE TABLE `tb_itempedido` (
  `ItemPedidoId` int(11) NOT NULL,
  `ProdutoId` int(11) DEFAULT NULL,
  `quantidade` int(11) DEFAULT NULL,
  `preco_unitario` int(11) NOT NULL,
  `PedidoId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `tb_pedido`
--

CREATE TABLE `tb_pedido` (
  `PedidoId` int(11) NOT NULL,
  `data_pedido` date DEFAULT NULL,
  `Cliente` varchar(240) DEFAULT NULL,
  `status_pedido` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `tb_pedido`
--

INSERT INTO `tb_pedido` (`PedidoId`, `data_pedido`, `Cliente`, `status_pedido`) VALUES
(1, '2023-05-03', 'dg', 'dsf'),
(2, '2023-05-03', 'dg', 'dsf'),
(3, '2023-05-03', 'dg', 'dsf'),
(4, '2023-05-03', 'dg', 'dsf'),
(5, '2023-05-03', 'dg', 'fexadisimo'),
(6, '2023-05-03', 'dg', 'dsf'),
(7, '2023-05-03', 'dg', '7'),
(8, '2023-05-03', 'dg', 'dsf'),
(9, '2023-05-03', 'dg', 'dsf'),
(11, '2023-05-03', 'dg', 'dsf'),
(12, '2023-05-03', 'dg', 'dsf'),
(13, '2023-05-03', 'dg', 'dsf'),
(14, '2023-05-03', 'dg', 'dsf'),
(15, '2023-05-03', 'dg', 'dsf'),
(16, '2023-05-03', 'dg', 'dsf'),
(17, '2023-05-03', 'dg', 'dsf'),
(18, '2023-05-03', 'dg', 'dsf'),
(19, '2023-05-13', 'diego', 'Em aberto');

-- --------------------------------------------------------

--
-- Estrutura para tabela `tb_produto`
--

CREATE TABLE `tb_produto` (
  `ProdutoId` int(11) NOT NULL,
  `nome` varchar(50) DEFAULT NULL,
  `preco` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `tb_produto`
--

INSERT INTO `tb_produto` (`ProdutoId`, `nome`, `preco`) VALUES
(1, 'Produto 1', 10.99),
(2, 'x2', 10.99),
(3, 's3', 10.99),
(4, 'Produto 4', 10.99),
(5, 'Produto 5', 10.99),
(6, 'Produto 6', 10.99),
(7, 'Produto 7', 10.99),
(8, 'Produto 8', 10.99),
(9, 'Produto 9', 10.99);

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `tb_itempedido`
--
ALTER TABLE `tb_itempedido`
  ADD PRIMARY KEY (`ItemPedidoId`),
  ADD KEY `fk_tb_itempedido_tb_produto` (`ProdutoId`),
  ADD KEY `fk_tb_itempedido_tb_pedido` (`PedidoId`);

--
-- Índices de tabela `tb_pedido`
--
ALTER TABLE `tb_pedido`
  ADD PRIMARY KEY (`PedidoId`);

--
-- Índices de tabela `tb_produto`
--
ALTER TABLE `tb_produto`
  ADD PRIMARY KEY (`ProdutoId`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `tb_itempedido`
--
ALTER TABLE `tb_itempedido`
  MODIFY `ItemPedidoId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `tb_pedido`
--
ALTER TABLE `tb_pedido`
  MODIFY `PedidoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT de tabela `tb_produto`
--
ALTER TABLE `tb_produto`
  MODIFY `ProdutoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Restrições para tabelas despejadas
--

--
-- Restrições para tabelas `tb_itempedido`
--
ALTER TABLE `tb_itempedido`
  ADD CONSTRAINT `fk_tb_itempedido_tb_pedido` FOREIGN KEY (`PedidoId`) REFERENCES `tb_pedido` (`PedidoId`),
  ADD CONSTRAINT `fk_tb_itempedido_tb_produto` FOREIGN KEY (`ProdutoId`) REFERENCES `tb_produto` (`ProdutoId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
