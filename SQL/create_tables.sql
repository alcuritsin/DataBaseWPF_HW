CREATE TABLE table_account
(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT ,
    login TEXT NOT NULL,
    password TEXT NOT NULL,
    is_active INTEGER NOT NULL DEFAULT 1
);

CREATE TABLE  table_user
(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    FOREIGN KEY (id) REFERENCES table_account(id)
        ON UPDATE NO ACTION
        ON DELETE  NO ACTION
);

CREATE TABLE table_email
(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    user_id INTEGER NOT NULL,
    email TEXT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES table_user(id)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE TABLE table_role
(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    role TEXT NOT NULL
);

CREATE TABLE table_account_role
(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    account_id INTEGER NOT NULL,
    role_id INTEGER NOT NULL,
    FOREIGN KEY (account_id) REFERENCES table_account(id)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    FOREIGN KEY (role_id) REFERENCES table_role(id)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

INSERT INTO table_account (login, password)
VALUES ('admin', '123');
INSERT INTO table_account (login, password, is_active)
VALUES ('administrator', '123', 0);
INSERT INTO table_account (login, password)
VALUES ('anst', '12345');

INSERT INTO table_user (first_name, last_name)
VALUES ('Admin', 'Admin');
INSERT INTO table_user (first_name, last_name)
VALUES ('Admin', 'Admin');
INSERT INTO table_user (first_name, last_name)
VALUES ('Andrey', 'Starinin');

INSERT INTO table_email (user_id, email)
VALUES (1, 'adm@adm.adm');
INSERT INTO table_email (user_id, email)
VALUES (3, 'anst@adm.adm');

INSERT INTO table_role (role)
VALUES ('admin');
INSERT INTO table_role (role)
VALUES ('user');

INSERT INTO table_account_role (account_id, role_id)
VALUES (1, 1);
INSERT INTO table_account_role (account_id, role_id)
VALUES (2, 1);
INSERT INTO table_account_role (account_id, role_id)
VALUES (3, 2);