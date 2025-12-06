
-- inserimento campo per gruppo nuove costanti GCO  
-- per microservizio rulesmngt

alter table afchr_int_schema.constantvalue add gco_constants text;
update afchr_int_schema.constantvalue set gco_constants = 'p0(ITH,DHB,CPT,DFC);p1(GPR,PIO);np2(GPR);np3(GAC,GBL,CCS,GHP,GCD,GCT,GTR,GCI,GPM,GCP,ITH)' where constant_id = 1;

alter table afchr_int_schema.constantvalue add gco_abbr_constants text;
update afchr_int_schema.constantvalue set gco_abbr_constants = '0503' where constant_id = 1;

