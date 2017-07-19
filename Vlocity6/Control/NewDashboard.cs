using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Vlocity6.Control
{
    public class NewDashboard
    {

        public string hostname;
        public string port;

        public NewDashboard()
        {
            this.hostname = "localhost";
            this.port = "13569";
        }
        public NewDashboard(string hostname, string port)
        {
            this.hostname = "localhost";
            this.port = "13569";
        }


        public void GetBenefitsSummaryData()
        {
            string requestURI = "/Vlocity/UI/VM/Requests/getBenefitsSummaryData.json";
            string requestURL = "http://" + this.hostname + ":" + this.port + requestURI;

            BDCSummaryRequest request = new BDCSummaryRequest();
            request.DayType = "7";
            request.EndGMTTime = "0";
            request.EndHour = "0";
            request.HourType = "5";
            request.PeakType = "7";
            request.StartGMTTime = "0";
            request.StartHour = "0";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string serializedRequest = jss.Serialize(request);
            string result = RequestHandler.Send(requestURL, serializedRequest);

           
            NewDashboard.BenefitsSummary objResponse = jss.Deserialize<NewDashboard.BenefitsSummary>(result);

            int read_IOEliminated = Int32.Parse(objResponse.HourTotals.rdextioreduce_iocnt) + Int32.Parse(objResponse.HourTotals.rdcachehits_iocnt);
            int write_IOEliminated = Int32.Parse(objResponse.HourTotals.wrtextioreduce_iocnt);
            int total_IOEliminated = Int32.Parse(objResponse.HourTotals.wrtextioreduce_iocnt) + Int32.Parse(objResponse.HourTotals.rdextioreduce_iocnt) + Int32.Parse(objResponse.HourTotals.rdcachehits_iocnt);
            int fs_Consolided = Int32.Parse(objResponse.HourTotals.freespaceselimcnt);
            int frag_prevented_eliminated = Int32.Parse(objResponse.HourTotals.fragselimcnt) + Int32.Parse(objResponse.HourTotals.fragspreventcnt) + Int32.Parse(objResponse.HourTotals.fragselimcnt_jit);
            
            StreamWriter f = File.AppendText("Results.txt");
            f.WriteLine("Read I/Os Eliminated" + "  " + read_IOEliminated);
            f.WriteLine("Total I/Os Eliminated" + " " + total_IOEliminated);
            f.WriteLine("Write I/Os Eliminated" + " " + write_IOEliminated );
            f.WriteLine("Freespaces Consolidated" + "   " + fs_Consolided);
            f.WriteLine("FragmentsPreventedAndEliminated" + " " + frag_prevented_eliminated);
            f.Close();
       

        }

        public class IOReduce
        {
            public string fragshnd { get; set; }
            public string iocnt_min { get; set; }
            public string iocnt_max { get; set; }
            public string iodensity_min { get; set; }
            public string iodensity_max { get; set; }
            public string phyiocnt { get; set; }
            public string ios_per_fragshnd { get; set; }
            public string maxiort { get; set; }
            public string read_iosaved { get; set; }
            public string write_iosaved { get; set; }
            public string savings { get; set; }
            public string iodensity_reduce_pct { get; set; }
            public string iodenspct { get; set; }
            public string splitios_reduce_pct { get; set; }
            public string frag_reduce_pct { get; set; }
            public string io_reduce_pct { get; set; }
        }

        public class CTBDTInfo
        {
            public string sysname { get; set; }
            public string GMTTableInitTime { get; set; }
            public string LocalTableInitTime { get; set; }
            public string numrecs { get; set; }
            public string badreccnt { get; set; }
            public string tblrecsize { get; set; }
            public string tblrecrsvd { get; set; }
            public string version { get; set; }
            public IOReduce IOReduce { get; set; }
        }

        public class Totals
        {
            public string rechour { get; set; }
            public string numsets { get; set; }
            public string numslices { get; set; }
            public string numsecs { get; set; }
            public string localhour { get; set; }
            public string features { get; set; }
            public string cputotal { get; set; }
            public string cpumin { get; set; }
            public string cpumax { get; set; }
            public string ostype { get; set; }
            public string osmajorver { get; set; }
            public string osminorver { get; set; }
            public string sysarch { get; set; }
            public string systype { get; set; }
            public string fragspreventcnt { get; set; }
            public string fragspreventbytes { get; set; }
            public string fragselimcnt { get; set; }
            public string fragselimbytes { get; set; }
            public string fragselimcnt_jit { get; set; }
            public string fragselimbytes_jit { get; set; }
            public string freespaceselimcnt { get; set; }
            public string freespaceselimbytes { get; set; }
            public string freereclaimbytes { get; set; }
            public string cachesize_total_mb { get; set; }
            public string cachesize_maxmem_mb { get; set; }
            public string cachesize_curmem_mb { get; set; }
            public string cachesize_curmem_mb_min { get; set; }
            public string cachesize_curmem_mb_max { get; set; }
            public string cachesize_disks_mb { get; set; }
            public string sysmemsize_total_mb { get; set; }
            public string sysmemsize_avail_mb { get; set; }
            public string sysmemsize_avail_mb_min { get; set; }
            public string sysmemsize_avail_mb_max { get; set; }
            public string rdiocnt { get; set; }
            public string rdiotime_usecs { get; set; }
            public string rdiosize_bytes { get; set; }
            public string rdcachehits_iocnt { get; set; }
            public string rdcachehits_bytes { get; set; }
            public string rdextioreduce_iocnt { get; set; }
            public string rdextioreduce_bytes { get; set; }
            public string rdqueuedepth { get; set; }
            public string rdsplitios { get; set; }
            public string wrtiocnt { get; set; }
            public string wrtiotime_usecs { get; set; }
            public string wrtiosize_bytes { get; set; }
            public string wrtcachehits_iocnt { get; set; }
            public string wrtcachehits_bytes { get; set; }
            public string wrtextioreduce_iocnt { get; set; }
            public string wrtextioreduce_bytes { get; set; }
            public string wrtqueuedepth { get; set; }
            public string wrtsplitios { get; set; }
            public string totiocnt { get; set; }
            public string totiotime_usecs { get; set; }
            public string totiosize_bytes { get; set; }
            public string totcachehits_iocnt { get; set; }
            public string totcachehits_bytes { get; set; }
            public string totextioreduce_iocnt { get; set; }
            public string totextioreduce_bytes { get; set; }
            public string totqueuedepth { get; set; }
            public string totsplitios { get; set; }
            public string totavgiops { get; set; }
            public string totavgbpsec { get; set; }
            public string totavgiort { get; set; }
            public string totavgphyiort { get; set; }
            public string totavgqueuedepth { get; set; }
            public string totavgsplitios { get; set; }
            public string totavgrdiopct { get; set; }
            public string totminiops { get; set; }
            public string totminbpsec { get; set; }
            public string totminiort { get; set; }
            public string totminphyiort { get; set; }
            public string totminqueuedepth { get; set; }
            public string totminsplitios { get; set; }
            public string totminrdiopct { get; set; }
            public string totmaxiops { get; set; }
            public string totmaxbpsec { get; set; }
            public string totmaxiort { get; set; }
            public string totmaxphyiort { get; set; }
            public string totmaxqueuedepth { get; set; }
            public string totmaxsplitios { get; set; }
            public string totmaxrdiopct { get; set; }
            public string iocnt { get; set; }
            public string iotime { get; set; }
            public string iosize { get; set; }
            public string iocntpct { get; set; }
            public string iotimepct { get; set; }
            public string iosizepct { get; set; }
        }

        public class HourTotals
        {
            public string rechour { get; set; }
            public string numsets { get; set; }
            public string numslices { get; set; }
            public string numsecs { get; set; }
            public string localhour { get; set; }
            public string features { get; set; }
            public string cputotal { get; set; }
            public string cpumin { get; set; }
            public string cpumax { get; set; }
            public string ostype { get; set; }
            public string osmajorver { get; set; }
            public string osminorver { get; set; }
            public string sysarch { get; set; }
            public string systype { get; set; }
            public string fragspreventcnt { get; set; }
            public string fragspreventbytes { get; set; }
            public string fragselimcnt { get; set; }
            public string fragselimbytes { get; set; }
            public string fragselimcnt_jit { get; set; }
            public string fragselimbytes_jit { get; set; }
            public string freespaceselimcnt { get; set; }
            public string freespaceselimbytes { get; set; }
            public string freereclaimbytes { get; set; }
            public string cachesize_total_mb { get; set; }
            public string cachesize_maxmem_mb { get; set; }
            public string cachesize_curmem_mb { get; set; }
            public string cachesize_curmem_mb_min { get; set; }
            public string cachesize_curmem_mb_max { get; set; }
            public string cachesize_disks_mb { get; set; }
            public string sysmemsize_total_mb { get; set; }
            public string sysmemsize_avail_mb { get; set; }
            public string sysmemsize_avail_mb_min { get; set; }
            public string sysmemsize_avail_mb_max { get; set; }
            public string rdiocnt { get; set; }
            public string rdiotime_usecs { get; set; }
            public string rdiosize_bytes { get; set; }
            public string rdcachehits_iocnt { get; set; }
            public string rdcachehits_bytes { get; set; }
            public string rdextioreduce_iocnt { get; set; }
            public string rdextioreduce_bytes { get; set; }
            public string rdqueuedepth { get; set; }
            public string rdsplitios { get; set; }
            public string wrtiocnt { get; set; }
            public string wrtiotime_usecs { get; set; }
            public string wrtiosize_bytes { get; set; }
            public string wrtcachehits_iocnt { get; set; }
            public string wrtcachehits_bytes { get; set; }
            public string wrtextioreduce_iocnt { get; set; }
            public string wrtextioreduce_bytes { get; set; }
            public string wrtqueuedepth { get; set; }
            public string wrtsplitios { get; set; }
            public string totiocnt { get; set; }
            public string totiotime_usecs { get; set; }
            public string totiosize_bytes { get; set; }
            public string totcachehits_iocnt { get; set; }
            public string totcachehits_bytes { get; set; }
            public string totextioreduce_iocnt { get; set; }
            public string totextioreduce_bytes { get; set; }
            public string totqueuedepth { get; set; }
            public string totsplitios { get; set; }
            public string totavgiops { get; set; }
            public string totavgbpsec { get; set; }
            public string totavgiort { get; set; }
            public string totavgphyiort { get; set; }
            public string totavgqueuedepth { get; set; }
            public string totavgsplitios { get; set; }
            public string totavgrdiopct { get; set; }
            public string totminiops { get; set; }
            public string totminbpsec { get; set; }
            public string totminiort { get; set; }
            public string totminphyiort { get; set; }
            public string totminqueuedepth { get; set; }
            public string totminsplitios { get; set; }
            public string totminrdiopct { get; set; }
            public string totmaxiops { get; set; }
            public string totmaxbpsec { get; set; }
            public string totmaxiort { get; set; }
            public string totmaxphyiort { get; set; }
            public string totmaxqueuedepth { get; set; }
            public string totmaxsplitios { get; set; }
            public string totmaxrdiopct { get; set; }
            public string iocnt { get; set; }
            public string iotime { get; set; }
            public string iosize { get; set; }
            public string iocntpct { get; set; }
            public string iotimepct { get; set; }
            public string iosizepct { get; set; }
        }

        public class HourlyData
        {
            public string rechour { get; set; }
            public string numsets { get; set; }
            public string numslices { get; set; }
            public string numsecs { get; set; }
            public string localhour { get; set; }
            public string features { get; set; }
            public string cputotal { get; set; }
            public string cpumin { get; set; }
            public string cpumax { get; set; }
            public string ostype { get; set; }
            public string osmajorver { get; set; }
            public string osminorver { get; set; }
            public string sysarch { get; set; }
            public string systype { get; set; }
            public string fragspreventcnt { get; set; }
            public string fragspreventbytes { get; set; }
            public string fragselimcnt { get; set; }
            public string fragselimbytes { get; set; }
            public string fragselimcnt_jit { get; set; }
            public string fragselimbytes_jit { get; set; }
            public string freespaceselimcnt { get; set; }
            public string freespaceselimbytes { get; set; }
            public string freereclaimbytes { get; set; }
            public string cachesize_total_mb { get; set; }
            public string cachesize_maxmem_mb { get; set; }
            public string cachesize_curmem_mb { get; set; }
            public string cachesize_curmem_mb_min { get; set; }
            public string cachesize_curmem_mb_max { get; set; }
            public string cachesize_disks_mb { get; set; }
            public string sysmemsize_total_mb { get; set; }
            public string sysmemsize_avail_mb { get; set; }
            public string sysmemsize_avail_mb_min { get; set; }
            public string sysmemsize_avail_mb_max { get; set; }
            public string rdiocnt { get; set; }
            public string rdiotime_usecs { get; set; }
            public string rdiosize_bytes { get; set; }
            public string rdcachehits_iocnt { get; set; }
            public string rdcachehits_bytes { get; set; }
            public string rdextioreduce_iocnt { get; set; }
            public string rdextioreduce_bytes { get; set; }
            public string rdqueuedepth { get; set; }
            public string rdsplitios { get; set; }
            public string wrtiocnt { get; set; }
            public string wrtiotime_usecs { get; set; }
            public string wrtiosize_bytes { get; set; }
            public string wrtcachehits_iocnt { get; set; }
            public string wrtcachehits_bytes { get; set; }
            public string wrtextioreduce_iocnt { get; set; }
            public string wrtextioreduce_bytes { get; set; }
            public string wrtqueuedepth { get; set; }
            public string wrtsplitios { get; set; }
            public string totiocnt { get; set; }
            public string totiotime_usecs { get; set; }
            public string totiosize_bytes { get; set; }
            public string totcachehits_iocnt { get; set; }
            public string totcachehits_bytes { get; set; }
            public string totextioreduce_iocnt { get; set; }
            public string totextioreduce_bytes { get; set; }
            public string totqueuedepth { get; set; }
            public string totsplitios { get; set; }
            public string totavgiops { get; set; }
            public string totavgbpsec { get; set; }
            public string totavgiort { get; set; }
            public string totavgphyiort { get; set; }
            public string totavgqueuedepth { get; set; }
            public string totavgsplitios { get; set; }
            public string totavgrdiopct { get; set; }
            public string totminiops { get; set; }
            public string totminbpsec { get; set; }
            public string totminiort { get; set; }
            public string totminphyiort { get; set; }
            public string totminqueuedepth { get; set; }
            public string totminsplitios { get; set; }
            public string totminrdiopct { get; set; }
            public string totmaxiops { get; set; }
            public string totmaxbpsec { get; set; }
            public string totmaxiort { get; set; }
            public string totmaxphyiort { get; set; }
            public string totmaxqueuedepth { get; set; }
            public string totmaxsplitios { get; set; }
            public string totmaxrdiopct { get; set; }
            public string iocnt { get; set; }
            public string iotime { get; set; }
            public string iosize { get; set; }
            public string iocntpct { get; set; }
            public string iotimepct { get; set; }
            public string iosizepct { get; set; }
        }

        public class PeaksHour
        {
            public string PeakIndex { get; set; }
        }

        public class CurRec
        {
            public string rechour { get; set; }
            public string numsets { get; set; }
            public string numslices { get; set; }
            public string numsecs { get; set; }
            public string localhour { get; set; }
            public string features { get; set; }
            public string cputotal { get; set; }
            public string cpumin { get; set; }
            public string cpumax { get; set; }
            public string ostype { get; set; }
            public string osmajorver { get; set; }
            public string osminorver { get; set; }
            public string sysarch { get; set; }
            public string systype { get; set; }
            public string fragspreventcnt { get; set; }
            public string fragspreventbytes { get; set; }
            public string fragselimcnt { get; set; }
            public string fragselimbytes { get; set; }
            public string fragselimcnt_jit { get; set; }
            public string fragselimbytes_jit { get; set; }
            public string freespaceselimcnt { get; set; }
            public string freespaceselimbytes { get; set; }
            public string freereclaimbytes { get; set; }
            public string cachesize_total_mb { get; set; }
            public string cachesize_maxmem_mb { get; set; }
            public string cachesize_curmem_mb { get; set; }
            public string cachesize_curmem_mb_min { get; set; }
            public string cachesize_curmem_mb_max { get; set; }
            public string cachesize_disks_mb { get; set; }
            public string sysmemsize_total_mb { get; set; }
            public string sysmemsize_avail_mb { get; set; }
            public string sysmemsize_avail_mb_min { get; set; }
            public string sysmemsize_avail_mb_max { get; set; }
            public string rdiocnt { get; set; }
            public string rdiotime_usecs { get; set; }
            public string rdiosize_bytes { get; set; }
            public string rdcachehits_iocnt { get; set; }
            public string rdcachehits_bytes { get; set; }
            public string rdextioreduce_iocnt { get; set; }
            public string rdextioreduce_bytes { get; set; }
            public string rdqueuedepth { get; set; }
            public string rdsplitios { get; set; }
            public string wrtiocnt { get; set; }
            public string wrtiotime_usecs { get; set; }
            public string wrtiosize_bytes { get; set; }
            public string wrtcachehits_iocnt { get; set; }
            public string wrtcachehits_bytes { get; set; }
            public string wrtextioreduce_iocnt { get; set; }
            public string wrtextioreduce_bytes { get; set; }
            public string wrtqueuedepth { get; set; }
            public string wrtsplitios { get; set; }
            public string totiocnt { get; set; }
            public string totiotime_usecs { get; set; }
            public string totiosize_bytes { get; set; }
            public string totcachehits_iocnt { get; set; }
            public string totcachehits_bytes { get; set; }
            public string totextioreduce_iocnt { get; set; }
            public string totextioreduce_bytes { get; set; }
            public string totqueuedepth { get; set; }
            public string totsplitios { get; set; }
            public string totavgiops { get; set; }
            public string totavgbpsec { get; set; }
            public string totavgiort { get; set; }
            public string totavgphyiort { get; set; }
            public string totavgqueuedepth { get; set; }
            public string totavgsplitios { get; set; }
            public string totavgrdiopct { get; set; }
            public string totminiops { get; set; }
            public string totminbpsec { get; set; }
            public string totminiort { get; set; }
            public string totminphyiort { get; set; }
            public string totminqueuedepth { get; set; }
            public string totminsplitios { get; set; }
            public string totminrdiopct { get; set; }
            public string totmaxiops { get; set; }
            public string totmaxbpsec { get; set; }
            public string totmaxiort { get; set; }
            public string totmaxphyiort { get; set; }
            public string totmaxqueuedepth { get; set; }
            public string totmaxsplitios { get; set; }
            public string totmaxrdiopct { get; set; }
            public string iocnt { get; set; }
            public string iotime { get; set; }
            public string iosize { get; set; }
            public string iocntpct { get; set; }
            public string iotimepct { get; set; }
            public string iosizepct { get; set; }
        }

        public class BenefitsSummary
        {
            public string CTBenefitsSummary { get; set; }
            public string version { get; set; }
            public string TableInitTime { get; set; }
            public string GMTTableInitTime { get; set; }
            public string numrecs { get; set; }
            public string badreccnt { get; set; }
            public string iodenspct { get; set; }
            public string numhours { get; set; }
            public string numdays { get; set; }
            public string daytype { get; set; }
            public string startday { get; set; }
            public string endday { get; set; }
            public string hourtype { get; set; }
            public string starthour { get; set; }
            public string endhour { get; set; }
            public string peaktype { get; set; }
            public string features { get; set; }
            public string slicesecs { get; set; }
            public CTBDTInfo CTBDTInfo { get; set; }
            public Totals Totals { get; set; }
            public HourTotals HourTotals { get; set; }
            public List<HourlyData> HourlyData { get; set; }
            public List<PeaksHour> PeaksHours { get; set; }
            public CurRec CurRec { get; set; }
        }

        public class BDCSummaryRequest
        {
            public string DayType;
            public string EndGMTTime;
            public string EndHour;
            public string HourType;
            public string PeakType;
            public string StartGMTTime;
            public string StartHour;

        }

    }

}
