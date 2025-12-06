
-- schedulerconfiguration
ALTER TABLE afchr_int_schema.schedulerconfiguration ADD flag_sync text NULL;
update afchr_int_schema.schedulerconfiguration set flag_sync = 'N'
where scheduler_name = 'CATALOG_SCHEDULER';
update afchr_int_schema.schedulerconfiguration set flag_sync = '-'
where scheduler_name != 'CATALOG_SCHEDULER';

INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(40, 7, 21, 59, 'CATALOG_SCHEDULER', 0);

ALTER TABLE afchr_int_schema.schedulerconfiguration ADD sync_post_service text NULL;
update afchr_int_schema.schedulerconfiguration 
 set sync_post_service = 'extractioncustomsync'
 -- set sync_post_service = 'extractionfullsync'
 where scheduler_name = 'CATALOG_SCHEDULER'
;

-- hrschedulerconfiguration

ALTER TABLE afchr_int_schema.hrschedulerconfiguration ADD active int NULL;
update afchr_int_schema.hrschedulerconfiguration set active = 1;

ALTER TABLE afchr_int_schema.hrschedulerconfiguration ADD flag_sync text NULL;
update afchr_int_schema.hrschedulerconfiguration set flag_sync = 'N';

ALTER TABLE afchr_int_schema.hrschedulerconfiguration ADD sync_post_service text NULL;
update afchr_int_schema.hrschedulerconfiguration set sync_post_service = 'elaborationmasterdatafullsync';


-- #######################################################################################################
-- ROLLBACK schedulerconfiguration produzione

-- afchr_int_schema.schedulerconfiguration definition

-- Drop table

-- DROP TABLE afchr_int_schema.schedulerconfiguration;

CREATE TABLE afchr_int_schema.schedulerconfiguration (
	sched_config_id serial4 NOT NULL,
	day_of_month int4 NOT NULL,
	time_of_day int4 NOT NULL,
	interval_in_minutes int4 NULL,
	scheduler_name text NULL,
	active int2 NULL,
	CONSTRAINT "PK_schedulerconfiguration" PRIMARY KEY (sched_config_id)
);

INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(24, 1, 21, 59, 'CATALOG_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(25, 2, 21, 59, 'CATALOG_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(26, 3, 21, 59, 'CATALOG_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(27, 4, 21, 59, 'CATALOG_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(28, 5, 21, 59, 'CATALOG_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(23, 6, 21, 59, 'CATALOG_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(21, 1, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(12, 3, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(13, 5, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(30, 9, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(18, 13, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(15, 15, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(29, 21, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(16, 23, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(19, 26, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(20, 29, 23, 55, 'MASTER_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(11, 0, 0, 100, 'SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(10, 0, 0, 5, 'SNOW_SCHEDULER', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(34, 1, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(39, 2, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 0);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(32, 4, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(33, 8, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(35, 12, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(31, 16, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(36, 20, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(37, 24, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 1);
INSERT INTO afchr_int_schema.schedulerconfiguration
(sched_config_id, day_of_month, time_of_day, interval_in_minutes, scheduler_name, active)
VALUES(38, 28, -1, NULL, 'SVECCHIAMENTO_TAB_CLEANING', 1);

-- #######################################################################################################
-- ROLLBACK hrschedulerconfiguration produzione

-- afchr_int_schema.hrschedulerconfiguration definition

-- Drop table

-- DROP TABLE afchr_int_schema.hrschedulerconfiguration;

CREATE TABLE afchr_int_schema.hrschedulerconfiguration (
	scheduler_config_id serial4 NOT NULL,
	days_denied text NULL,
	hours_permitted text NULL,
	minute_permitted text NULL,
	min_interval text NULL,
	hrsync_type text NULL,
	hrsync_type_desc text NULL,
	config_args_request text NULL,
	config_args_request_desc text NULL,
	config_args_request_guide text NULL,
	CONSTRAINT "PK_hrschedulerconfiguration" PRIMARY KEY (scheduler_config_id)
);

INSERT INTO afchr_int_schema.hrschedulerconfiguration
(scheduler_config_id, days_denied, hours_permitted, minute_permitted, min_interval, hrsync_type, hrsync_type_desc, config_args_request, config_args_request_desc, config_args_request_guide)
VALUES(2, '1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31', '22', '00', '30', 'FULL', NULL, NULL, NULL, NULL);
INSERT INTO afchr_int_schema.hrschedulerconfiguration
(scheduler_config_id, days_denied, hours_permitted, minute_permitted, min_interval, hrsync_type, hrsync_type_desc, config_args_request, config_args_request_desc, config_args_request_guide)
VALUES(3, '2,4,6,7,8,10,11,12,14,16,17,18,19,20,22,24,25,27,28,30,31', '22', '00', '30', 'CUSTOM', 'Light', '[,Y,N,,Y,Y];[,init_month -3,L,N,Y,];[,,N,Y,Y,]', 'estrae nell ordine modify, dummy con changedateattribute da inizio 3 mesi precedenti e insert', '[Validitydate{Y => data ultimo run / null => nessuna data / YYYY-MM-DD => data specifica / -x => x giorni precedenti / init_month -x => da inizio x mese precedente }, [changeddateattribute{Y => data ultimo run / null => nessuna data / YYYY-MM-DD => data specifica / -x => x giorni precedenti / init_month -x => da inizio x mese precedente }, costcenterDummy{L / Y / N / null => N}, noCostcenter{ Y / N / null => N}, consistenti{ Y / N / null => N}, effettivi{ Y / N / null => N}];...');

