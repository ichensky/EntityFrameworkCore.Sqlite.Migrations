create table `User` (
`Name` text not null primary key unique,
`Created` datetime default current_timestamp
);
