create table `Ship` (
`Name` text not null primary key unique,
`Created` datetime default current_timestamp
);
create table `Dog` (
`Name` text not null primary key unique,
`Created` datetime default current_timestamp
);
