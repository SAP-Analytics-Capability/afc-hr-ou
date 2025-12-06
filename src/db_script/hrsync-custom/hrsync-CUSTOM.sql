
-- alter table afchr_int_schema.hrschedulerconfiguration rename to afchr_int_schema.hrschedulerconfiguration_orig;

alter table afchr_int_schema.hrschedulerconfiguration 
add column hrsync_type text NULL,
add column hrsync_type_desc text NULL,
add column config_args_request text NULL,
add column config_args_request_desc text NULL,
add column config_args_request_guide text NULL
;

UPDATE afchr_int_schema.hrschedulerconfiguration
SET hrsync_type='FULL'
WHERE scheduler_config_id=2;


INSERT INTO afchr_int_schema.hrschedulerconfiguration
(scheduler_config_id,days_denied, hours_permitted, minute_permitted, min_interval,hrsync_type,hrsync_type_desc,config_args_request
        ,config_args_request_desc,config_args_request_guide)
VALUES(3,'1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31', '21', '00', '30','CUSTOM','Light'
        ,'[Y,N,,Y,Y];[,L,N,Y,];[,N,Y,Y,]','estrae nell ordine modify, dummy e insert'
		,'[Validitydate{Y => data ultimo run / null => nessuna data / YYYY-MM-DD => data specifica / -x => x giorni precedenti / init_month -x => da inizio x mese precedente }, [changeddateattribute{Y => data ultimo run / null => nessuna data / YYYY-MM-DD => data specifica / -x => x giorni precedenti / init_month -x => da inizio x mese precedente }, costcenterDummy{L / Y / N / null => N}, noCostcenter{ Y / N / null => N}, consistenti{ Y / N / null => N}, effettivi{ Y / N / null => N}];...');



-- ------------------------ --
-- solo in caso di ROLLBACK --
-- ------------------------ --

-- afchr_int_schema.hrschedulerconfiguration definition

-- Drop table

-- DROP TABLE afchr_int_schema.hrschedulerconfiguration;

CREATE TABLE afchr_int_schema.hrschedulerconfiguration (
	scheduler_config_id serial4 NOT NULL,
	days_denied text NULL,
	hours_permitted text NULL,
	minute_permitted text NULL,
	min_interval text NULL,
	CONSTRAINT "PK_hrschedulerconfiguration" PRIMARY KEY (scheduler_config_id)
);


-- sviluppo
INSERT INTO afchr_int_schema.hrschedulerconfiguration
(scheduler_config_id, days_denied, hours_permitted, minute_permitted, min_interval)
VALUES(1, '1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31', '10,11,12,13,14,15,18', '30', '59');


-- produzione
INSERT INTO afchr_int_schema.hrschedulerconfiguration
(scheduler_config_id, days_denied, hours_permitted, minute_permitted, min_interval)
VALUES('2,4,6,7,8,10,11,12,14,16,17,18,19,20,22,24,25,27,28,30,31', '21', '00', '30');





