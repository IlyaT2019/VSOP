using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{   
    public enum TypeVariable
    {
        Default = 0,
        //[Description("qwee")]
        _string = 1,
        //[Description("add")]
        _double = 2, 
        //[Description("zxc")]
        _int = 3,        
        //[Description("cvbcvb")]
        _datatable = 4,

        Point = 5
    }
    
    public enum NameAppVariable
    {
        Default = 0,
        WorkDir,
        BaseTable,
        SortTable,
        FirstPoint,
        MiddlePoint,
        FinishPoint,
        MaxPoint,
        CountPoints,
        A_pr,
        B_pr,
        D,
        D1,
        A,
        Amin,
        Amax,               
        Nmin,
        Nmax
    }

    public enum NameAppVariableCommand
    {
        Default = 0,
        SortingTable,
        GetFirstPoint,
        GetMiddlePoint,
        GetFinishPoint,
        GetMaxPoint,
        GetApr,
        GetBpr,
        GetD,
        GetD1,
        isNotEqualByFirstAndFinishPoint,
        isEqualFirstPointByXAndNotEqualPointByZ_1,
        isEqualFirstPointByXAndNotEqualPointByZ_2,
        isEqualFirstPointByXAndNotEqualPointByZ_3,
        isEqualFirstPointByXAndNotEqualPointByZ_4,
        ListIf,
        Analis
    }
}
