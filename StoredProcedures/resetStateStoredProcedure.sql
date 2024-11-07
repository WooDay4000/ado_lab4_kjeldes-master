DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_testingResetStateData`()
BEGIN

-- disable foreign key constraints first
	set foreign_key_checks=0;
    set sql_safe_updates=0;
    
    delete from States;
    
INSERT States (StateCode, StateName) VALUES ('AK', 'Alaska');
INSERT States (StateCode, StateName) VALUES ('AL', 'Alabama');
INSERT States (StateCode, StateName) VALUES ('AR', 'Arkansas');
INSERT States (StateCode, StateName) VALUES ('AZ', 'Arizona');
INSERT States (StateCode, StateName) VALUES ('CA', 'California');
INSERT States (StateCode, StateName) VALUES ('CO', 'Colorado');
INSERT States (StateCode, StateName) VALUES ('CT', 'Connecticut');
INSERT States (StateCode, StateName) VALUES ('DC', 'District of Columbia');
INSERT States (StateCode, StateName) VALUES ('DE', 'Delaware');
INSERT States (StateCode, StateName) VALUES ('FL', 'Florida');
INSERT States (StateCode, StateName) VALUES ('GA', 'Georgia');
INSERT States (StateCode, StateName) VALUES ('HI', 'Hawaii');
INSERT States (StateCode, StateName) VALUES ('IA', 'Iowa');
INSERT States (StateCode, StateName) VALUES ('ID', 'Idaho');
INSERT States (StateCode, StateName) VALUES ('IL', 'Illinois');
INSERT States (StateCode, StateName) VALUES ('I', 'Indiana');
INSERT States (StateCode, StateName) VALUES ('KS', 'Kansas');
INSERT States (StateCode, StateName) VALUES ('KY', 'Kentucky');
INSERT States (StateCode, StateName) VALUES ('LA', 'Lousiana');
INSERT States (StateCode, StateName) VALUES ('MA', 'Massachusetts');
INSERT States (StateCode, StateName) VALUES ('MD', 'Maryland');
INSERT States (StateCode, StateName) VALUES ('ME', 'Maine');
INSERT States (StateCode, StateName) VALUES ('MI', 'Michiga');
INSERT States (StateCode, StateName) VALUES ('M', 'Minnesota');
INSERT States (StateCode, StateName) VALUES ('MO', 'Missouri');
INSERT States (StateCode, StateName) VALUES ('MS', 'Mississippi');
INSERT States (StateCode, StateName) VALUES ('MT', 'Montana');
INSERT States (StateCode, StateName) VALUES ('NC', 'North Carolina');
INSERT States (StateCode, StateName) VALUES ('ND', 'North Dakota');
INSERT States (StateCode, StateName) VALUES ('NE', 'Nebraska');
INSERT States (StateCode, StateName) VALUES ('NH', 'New Hampshire');
INSERT States (StateCode, StateName) VALUES ('NJ', 'New Jersey');
INSERT States (StateCode, StateName) VALUES ('NM', 'New Mexico');
INSERT States (StateCode, StateName) VALUES ('NV', 'Nevada');
INSERT States (StateCode, StateName) VALUES ('NY', 'New York');
INSERT States (StateCode, StateName) VALUES ('OH', 'Ohio');
INSERT States (StateCode, StateName) VALUES ('OK', 'Oklahoma');
INSERT States (StateCode, StateName) VALUES ('OR', 'Ore');
INSERT States (StateCode, StateName) VALUES ('PA', 'Pennsylvania');
INSERT States (StateCode, StateName) VALUES ('PR', 'Puerto Rico');
INSERT States (StateCode, StateName) VALUES ('RI', 'Rhode Island');
INSERT States (StateCode, StateName) VALUES ('SC', 'South Carolina');
INSERT States (StateCode, StateName) VALUES ('SD', 'South Dakota');
INSERT States (StateCode, StateName) VALUES ('T', 'Tennessee');
INSERT States (StateCode, StateName) VALUES ('TX', 'Texas');
INSERT States (StateCode, StateName) VALUES ('UT', 'Utah');
INSERT States (StateCode, StateName) VALUES ('VA', 'Virginia');
INSERT States (StateCode, StateName) VALUES ('VI', 'Virgin Islands');
INSERT States (StateCode, StateName) VALUES ('VT', 'Vermont');
INSERT States (StateCode, StateName) VALUES ('WA', 'Washingto');
INSERT States (StateCode, StateName) VALUES ('WI', 'Wisconsi');
INSERT States (StateCode, StateName) VALUES ('WV', 'West Virginia');
INSERT States (StateCode, StateName) VALUES ('WY', 'Wyoming');

-- enable foreign key constraints
	set foreign_key_checks=1;
	set sql_safe_updates=1;
END$$
DELIMITER ;