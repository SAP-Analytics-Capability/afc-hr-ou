
-- inserimento costanti  
-- GCO, SMD, GAT, GBI, GCC, GCH, GCR, GCS, GFR, GOI
-- per microservizio rulesmngt

alter table afchr_int_schema.constantvalue add gco text;
alter table afchr_int_schema.constantvalue add smd text;

update afchr_int_schema.constantvalue set gco = 'GCO', smd ='SMD' where constant_id = 1;

alter table afchr_int_schema.constantvalue add gat text;
alter table afchr_int_schema.constantvalue add gbi text;
alter table afchr_int_schema.constantvalue add gcc text;
alter table afchr_int_schema.constantvalue add gch text;
alter table afchr_int_schema.constantvalue add gcr text;
alter table afchr_int_schema.constantvalue add gcs text;
alter table afchr_int_schema.constantvalue add gfr text;
alter table afchr_int_schema.constantvalue add goi text;

update afchr_int_schema.constantvalue 
set gat = 'GAT'
 ,gbi = 'GBI'
 ,gcc = 'GCC'
 ,gch = 'GCH'
 ,gcr = 'GCR'
 ,gcs = 'GCS'
 ,gfr = 'GFR'
 ,goi = 'GOI' 
where constant_id = 1;

-- 13/09/2022
alter table afchr_int_schema.constantvalue add gpq text;
update afchr_int_schema.constantvalue 
set gpq = 'GPQ'
where constant_id = 1;

