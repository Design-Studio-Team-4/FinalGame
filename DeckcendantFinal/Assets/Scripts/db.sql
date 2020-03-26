use christopherwclar;

create table scores ( 
PlayThroughID int(11) NOT NULL auto_increment,
PlayerName varchar (25) default null,
Score int (11) default null,
primary key (PlayThroughID));

insert into scores(PlayerName,Score) values ('Chris',1);

select PlayerName, Score From scores;



