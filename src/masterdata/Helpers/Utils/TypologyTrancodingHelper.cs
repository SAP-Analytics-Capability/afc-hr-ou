using System;
using System.Linq;
using System.Collections.Generic;
using masterdata.Models;

namespace masterdata.Helpers
{
    public class TypologyTranscodingHelper
    {
        public static ConstantValue GetConstants(List<TypologyTranscoding> list)
        {
            ConstantValue output = new ConstantValue();

            try
            {
                var gds = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GDS"));
                if (gds != null)
                {
                    output.gds = ((TypologyTranscoding)gds).SnowCode;
                }

                var sesec = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("SSE"));
                if (sesec != null)
                {
                    output.sesec = ((TypologyTranscoding)sesec).SnowCode;
                }

                var dighub = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("DGH"));
                if (dighub != null)
                {
                    output.dighub = ((TypologyTranscoding)dighub).SnowCode;
                }

                var peo = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("HRO"));
                if (peo != null)
                {
                    output.peo = ((TypologyTranscoding)peo).SnowCode;
                }

                var chapter = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("CHP"));
                if (chapter != null)
                {
                    output.chapter = ((TypologyTranscoding)chapter).SnowCode;
                }

                var digfac = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("DGF"));
                if (digfac != null)
                {
                    output.digfac = ((TypologyTranscoding)digfac).SnowCode;
                }

                var procurement = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("PRO"));
                if (procurement != null)
                {
                    output.procurement = ((TypologyTranscoding)procurement).SnowCode;
                }

                var communications = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("CCO"));
                if (communications != null)
                {
                    output.communications = ((TypologyTranscoding)communications).SnowCode;
                }

                var afc = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("AFC"));
                if (afc != null)
                {
                    output.afc = ((TypologyTranscoding)afc).SnowCode;
                }
                var mbl = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("MBL"));
                if (mbl != null)
                {
                    output.attrPrevBsMl = ((TypologyTranscoding)mbl).SnowCode;
                }

                var gco = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GCO"));
                if (gco != null)
                {
                    output.gco = ((TypologyTranscoding)gco).SnowCode;
                }

                var smd = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("SMD"));
                if (smd != null)
                {
                    output.smd = ((TypologyTranscoding)smd).SnowCode;
                }


                var gat = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GAT"));
                if (gat != null)
                {
                    output.gat = ((TypologyTranscoding)gat).SnowCode;
                }

                var gbi = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GBI"));
                if (gbi != null)
                {
                    output.gbi = ((TypologyTranscoding)gbi).SnowCode;
                }

                var gcc = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GCC"));
                if (gcc != null)
                {
                    output.gcc = ((TypologyTranscoding)gcc).SnowCode;
                }

                var gch = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GCH"));
                if (gch != null)
                {
                    output.gch = ((TypologyTranscoding)gch).SnowCode;
                }

                var gcr = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GCR"));
                if (gcr != null)
                {
                    output.gcr = ((TypologyTranscoding)gcr).SnowCode;
                }

                var gcs = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GCS"));
                if (gcs != null)
                {
                    output.gcs = ((TypologyTranscoding)gcs).SnowCode;
                }

                var gfr = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GFR"));
                if (gfr != null)
                {
                    output.gfr = ((TypologyTranscoding)gfr).SnowCode;
                }

                var goi = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GOI"));
                if (goi != null)
                {
                    output.goi = ((TypologyTranscoding)goi).SnowCode;
                }

                var gpq = list.FirstOrDefault(e => e.ParentName.ToUpper().Equals("GPQ"));
                if (gpq != null)
                {
                    output.gpq = ((TypologyTranscoding)gpq).SnowCode;
                }

            }
            catch (Exception) { }
            return output;
        }
    }
}
