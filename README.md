# Ecommerce
Banco sql
CREATE TABLE tb_usuario (
  UsuarioId INT PRIMARY KEY,
  nome VARCHAR(50),
  email VARCHAR(50),
  senha VARCHAR(50)
);

CREATE TABLE tb_produto (
  ProdutoId INT PRIMARY KEY,
  nome VARCHAR(50),
  preco DECIMAL(10,2)
);

CREATE TABLE tb_pedido (
  Id INT PRIMARY KEY,
  data_pedido DATE,
  Cliente INT,
  status_pedido VARCHAR(50),
  FOREIGN KEY (Cliente) REFERENCES tb_usuario(UsuarioId)
);

CREATE TABLE tb_itempedido (
  Id INT PRIMARY KEY,
  Produto INT,
  quantidade INT,
  preco_unitario DECIMAL(10,2),
  Pedido INT,
  FOREIGN KEY (Produto) REFERENCES tb_produto(ProdutoId),
  FOREIGN KEY (Pedido) REFERENCES tb_pedido(Id)
);
