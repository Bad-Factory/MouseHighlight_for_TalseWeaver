using Hook;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace MouseHighlight
{
    public partial class Form1 : Form
    {
        #region [1. 필드 및 속성 분류 (Variables & Properties)]

        #region [1-1. GDI 폼 및 오버레이 관리 필드]
        /// <summary>마우스 커서 오버레이를 표시하는 GDI 폼</summary>
        private GdiForm mCursorForm;
        /// <summary>마우스 클릭 효과를 표시하는 GDI 폼</summary>
        private GdiForm mClickCursorForm;
        /// <summary>스킬 범위 오버레이를 표시하는 GDI 폼</summary>
        private GdiForm mSkillForm;
        #endregion

        #region [1-2. 마우스 비트맵 캐시 필드]
        /// <summary>마우스 커서 비트맵 이미지</summary>
        private static Bitmap mBitmap_Mouse;
        /// <summary>마우스 클릭 효과 비트맵 이미지</summary>
        private static Bitmap mBitmap_Click;
        /// <summary>스킬 범위 오버레이 비트맵 이미지</summary>
        private static Bitmap mBitmap_Skill;
        #endregion

        #region [1-3. 폼 상태 및 설정 파일 경로 필드]
        /// <summary>폼 로드 완료 여부를 나타내는 플래그</summary>
        private bool mIsLoadForm;
        /// <summary>설정 파일(INI)의 전체 경로</summary>
        private string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MouseHighlight_TalseWeaver_settings.ini");
        #endregion

        #region [1-4. 커서 프리셋 상수]
        /// <summary>기본 커서 프리셋 Base64 데이터</summary>
        private const string PRESET_DEFAULT = "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAMAAAD04JH5AAACplBMVEWYcBVdVUlDKyFmZmaZXxWagUmlm3jAvbPY2t6rdBswIRxlSi3s10EjIyTMzMyCfXDZszSZmZlbSjVYVlbk2IRXQy0zMzP///+qbBeihzNHOCnOrizjuiicoKbBhBskJSmYimW3dhXkoxyEYi+Uj4VVMQZDOCzBxMfRlRrUoiJLS0v32SnkwzHa2dfttiHrwCLvzyq9fRivkjjgqx9ONSJ5bljMt0i0ub/Egxdza1yEWR2QdD6+iB5TRjvGtWDr6dwdHiEnJSePWRlRQDLFqkmnkkZoX1PRmR+HeVOSYyRKRkOUcS3isSDpyi60dxlUU1X09OPnuyJdPB2KYQ6kfSr45UC1qoNkWUHj5eUyKyR/f3/d29Hi26VENTO+nSwxIiV4VhP75TR5e3+gaRwoIR2plVlycnE6LyWchD8fHh56Z0KEemGzsKXx1S9vUCFISE7ayl2Gh4ZiX1pYU03MzJl9a0yDZz5NRj/p5s7zzSa3qnPqxiiHbTt8UCErKSjbvznarCO4gSHRpy7v6IRORDLesiKLd0nt6cDOkBn4xyHIjx5pWjxWPxPqvCBEJQg2Hg3ZlRiSeDjw2jiigDbiyzq9v79JQTY9MyMaFA+ATRWxsrEZGC3GmiPMt1dAQUSCho/AuZ6QkI1DQ0PZzIh0Z0ns0jVAOS/29u+rooj/7zbp6OPEuIji5OlsYk5hVEPXnRooJyiomGaEg3yxgSjPyq0sJTScbijl0U7X070NDQ+ZhlZwSiKofRi5jyhxVDGynVUsKCTFs2/u8fPivTQnJSNZTz2orbN2ZDWZj3XGqzvpyj/hzm1zb2fc1rFucnq1oUvLihdBPjzapR7txiRUVFtEPzjasyxmQgZSS0DmvyY5Oz+HaCl4eHjktyHUvUG3nTqxkSqYcBXiWRBuAAAA4nRSTlP///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////8AcB66HwAAGqRJREFUeJztW3k8lPv3f9tNyEQl2kmboiYthEIqSdlCXWRpGSpbVFK6LaIotJlWa4UoWRJCMi2EULZkbUEaKcvEWH7P6Ha/t3vLUvd7v78/7nn1ejzPZ848n3fnnM/5nOUz3NX43xIn/sf0L4D/JwB87+J/RRy9q4B1v51vIXjwP6DfVKCQnTBD9S6L5YJ/mj4D4LF/vhQNLbpaGXf/aQy/SSDz3Oih0hlUfNistXgWy+UfBMH9+Y8+XofcF5vJC15ep/EzeLnEWnf8QxbB8ZsrTl+ASo/8m0ulAWJkPKq9+YbFueK/T1/8gAVgkONRiX1vsWI8Mb92uveVoFn/gEV8kQBSIAYbJp9mZ2RHcJcFr3aBbOx42Vi029Qf+K8q43dP6KwkbcOcrnHRyLfgYbM0qVXmlTYKIjD5zvVUpfz/oiB+B7CMTmfCWUh+4i7Oy+PrF+gWNq48Les8vhq8J8kmuovv+rLwX6HfVQDKQ2gwSa7FTy5YzfQvTkXCvbZAjD5TSHxUPb5MJ/qYZ5obPPB30382o0wQIkBJpPMG+sLh+5+Ry8L8tkprcEntlkEHpKNBa9jzdOss979bEP+RADSjoQTSAcsx61slIzDs/cykk7qma68o2MUBZ6Kh08tU2LF74d8qCC6H32+rFcGbzSNS3O1rn66mVzV0hGD95HXu8ztK6ms6a2r5nacvfmxQ9OCD8an4BSyB8wvw99Af4gFXHToNyaEwCZUHw2YlFo99Law+I7MgqW10x7b7R01Gvk4bidDQSHQYJqjduuvrjr+D/iABuB2dbKhLWjX3precQbO3Q+TqhDeRH66OEpZ5ev2NYl77o8tC885YCMyda1onwxKXfTzP8uOcbi78JHH8MSpOUVIC6OqGAvr5ciepPquH3i0xthSnCAqiBVkOG4oTxo/veBP68NpbdF8HokNNpKkvftZPfRWSPYMnla6R1k1BGPc22t4NH18K9HRVil9TGprsyONjEXy83CPMuo23bcuWbTY2Bmyr3L6Q8FOyP+OnvgLQQ1eiKTEfzN+t4eHM7UA5MnR1bFzyrYu/SsiSucI/SEA4qGS0aFLSM7mbzL2vt9iY6JRhP+/JDz2UDF/Wjy7P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";
        /// <summary>블루 커서 프리셋 Base64 데이터</summary>
        private const string PRESET_BLUE = "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAMAAAD04JH5AAAC1lBMVEU0WIcbHSEuToFWhKwkhNsjKUChwdX///8dPY1kxewjaMsiS7QiX8QJHFc2OUB0qdEzMzPw9vIhWsJYeJYbQZsgbs8fOHk6jtTZ4+cuV52X1OmLkpsoMEMmMksxhckmKzRicIYqQXUdISolWr0kUazd3+EhSaF4gpM6huogVrVtcn4kNmwkYL97go1bXF1BRU5MZooOEy2ZmZloiKrMzMxcZHMlfdooKSwXQ7S8v8ItOlQgIi+vyNRIV3siWMZmmcxJz+9JSUpDhtNJqvh1m7tWWmOssLUuTZY+otg/QEJHWW88Q10wmuElgtBGdqEZL2IcJD85R2gjcckiZNcjYdktjd82dqlAZqGNvNFErO5Fg7Ypf8eGiItAtuuX5fFBXX1ShclLyPi51N+UnaeOorJ1d3o4UG5rh7Y2abgeJjlQWmcICQ7i8PAxUY5mZmYkIig2O0siYc2x2+bm6OnV19ozM2ZCwudctt0gLVYgTK1+wt+xtbsoMUBPj7UDDz0VKmQ5irkzPlO/y9IkQYYgU7VCqdZQbJggWt44RlohPnozddkumts3aawiec9OV2siWrZnpcEzZsw6SHJXZIIXKYFtuNI4U3pLaogmKS7f6e88XoW3vMMtWqs+VYJt0u2gqLErW5U0d8NARlUzmcytrrA2PUgiddMfIicmZ8IgZctSU1aczNwmVstGSlA3q+VFa5MzZpkvN0McGyxkk7J9foBCTmVUZHZleIknTqFK2vYWH0gkKjcrN0oTFRy2v8wkatkiJi7J4uoHEDZ7obeLrsqet8QhULw3d7YwRWClsbxKmsQiJCfV2t8lL1BsfJRfp9FBVnHQ2+RucHMcNn4hU7osk+B/tMwjbtdRW3UqSYlK6P0OJnUqeegypOvw8vPBxMghQ48hXdgznNhOo+GIkKI8P0VNUltLc6ZfcI6kqKwrRnAQK48+uOWfscRIUGF1jaN+j500WIdSmRWfAAAA8nRSTlP/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////AJA0f2kAABrtSURBVHic7Xt5PNT79/+ZsWZJm1DZQoxCmmyNtdKkbC2khMGkGLlKV/ZoEO1pRpFlEK0ShSxlS/akEkpXo9xooREi4fca9bnf221B3fv5/P64x8M85v2e8573833OeZ3X85zXazhZ8L8VLPyP5V8A/58AoCjC/0o4R18niVy5NgkOwv9APlrArHlKhVCw4q6SEvhvC+ZjHjD/7ZYZK5demOkxyUwH/pvyCUAOodHn7IoZg8DKjcnGm8F/D8SnUaCAC178asZtfTCzyFkqFJhGYe6C/458DEKQJEASREhQWVajh1OtBGcqRklIwj8vnwCAHKPh9+butPQzNIm3m0/CNv3CjTsssztW/eMR8R8A1EewnpXHKW7XXQsj04hmoA9CA0uBEnPWKv0fHZ5/ZMI23C15wzAP4E6XPbvNLHbk9Gl9sCq0GHDfwewvYf5zwxPzn+m4hE52BKHDBy+esNS1TorZsTH9lQOG/UGhPnrhYRhM+mcM8YcFdOzt6+VZEk67Q1rz+zXIiUYhfLdcXEbSrfTToRAGrNzv9/8jeYrzj3d5sKoFWxMpKmSy3Rc/b4HHa/PIrYd4zc7EOVilA2RgCNog9KsB/u9O2Jj/Y0Tmp3FTWWFrQjCXDxwZ1Jfbs+9xF3GyL76TOgJAzwDTDKRjiqFoR8Hfmaf+BICpDbgmeY3e/Fsnkiovd3TLHQwLxoI4k7tJXOBVK7giM7BBmALmVw/P/XT4e+RPfEAST2iChkQQWnKFpV65ubDjrVemuIBpo/PsQ/2rqx5jKHGtFFNTU8hIp4okNipSmPB3yJ8JiVKpqjxW/hflsBbJLgGdLRnUi4skuj/Qi0RxIvqEB8SFaxvzaLS4ONMcHkhnWVUI9pfs+vmoxPyZFVvF4BCgdav292eBznS9EaHu/ndmmkefv4R3EO29XykzXEY1lHiKzwHpJlsjh5jJR+z7yTz1GQCdJw3zIazm3XTPcvsGHUEuvSLTI/szFcSrk063VoU5zezaZrMTr73cPASgAVAQZJzZaLqx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";
        /// <summary>레드 커서 프리셋 Base64 데이터</summary>
        private const string PRESET_RED = "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAMAAAD04JH5AAACtVBMVEVmMzMYFRnAXlLdyMLfgWBeTlJqDxKbXll6FBZKSEmqrK+GUE8vHiFWFhnyPDJ1QkOnKyYkHSDfZkWBFxiZMzN4GRm7NyexKyA2IyWTREGUTkqaiImLMC6nlJa9HBaBEROlMijirZSOJSJEHB+ogIBFLTBdFxnMzMyGNTZTQkaZPzm1tLZYV1kzMzP///9mHB9mZmYxCAt1ZWiHiIzU1tnIQi7QVD9LDBDNXUbnvLaEbnA/MjSZmZnuRT1CFhkeHiG7LyLRwb9ULzFYHB+ZHxx9CQpORUiubWC+trjVurHMmZnMMCArJSe3aVt4d3jOSUMlIiRyODnp3dq0eXFoR0lGKStCP0FgERSKHBq7iX/BLSDa297RQzsmGx5/PTyTHx1kDxK+l5KGZWfQbFQ9Jym3IyLsiVo+OTxOIiXZgGfdRDq+PjhxExbf1dVYUFRyEBL/mWZ+KCjIqKKAgIGeHhpMNzpfPT+2m5pGOj5BAgOOjI53dnx2KywvJSdPMTQqKCrgb012W13o6u1AIiRuFhjIgG5mFhnUrqGAVVfQKBtPGh2KFhampajdOizplHBxRUeAGxukU0m9vb+0YEyhcnHj4+XARjVRUFKuLiPu7OiTExHJVEWnX1moJx6JfoLLPCjVTTKZZmZgFRiVJiWujIy0RDyBfX94Cw43HSC3OCpjICNxHiApDQ9JSE2fJR7Hdm0LCQwqICNmAABdWV5qWFy5jYxTICOXIx15cHOVRz5JJSd1MjODOjhyUFMcDhHMeVvXmH6CIyNBLzJCDhOxpKcjJCh9QkLBiHczAACLOTSOT05AP0NkS0+VeHmwOCq7U0HMMzPJSTTupHynFxbm5OOxODXt8vVXPD+ytbqOFBS0GxbszcPh0srgelhpFRfGs7QyJCZjPD7c4OPWVzlQBwpZBAVmMzNGBllKAAAA53RSTlP//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////wCyww0SAAAbIklEQVR4nO1beTzV2ft/WwrZqexJDCkRsqSyU9m7NdJCEpOtpslUFG2yTCZNiyJLRYMpLrJkzx5lTy5FpQVjxNhCtt9RM/Ntmgo18/3+/pj36+VzP/d6Pue873Oe85znPM+5jM3434Ie/2P8S+D/CQHjs/hfgfH1VQiRUUIIxP8AbzRgkSgT19xxNsA+B/9tvCGg2cDu1SPKn1bffPa/zYHujSPSv6h8kLdEJYBdSiWIWwNO+K/hNwIGKvoMs3jNp/ew94CQuNrvde6/ZBG/EcCuPSGrT45cYHNDfQ8Ih9KEcBkeWfzz+J2AgYrtKyxXmC8Vs8iHAoyToL607Rey0MQ/i98dUUon0tMLjOSpxqUwCZdiZ2fv0ds80KurYJxTjX8Sf3jC2pBWffucFihd+XlgSf0rE2oGO/k0wJQumRJpX52DfwoMLr/dtFnN/Barl0gssEla4Nxn3OFdeZKrkonpFdNj4Zrm9mvdHd8b4Z8A3R/xgP4yWNlT/KTL96cx/0rnuebcU35dxFhTIFXfw45LPiqvZDqgib8b/1mM5sHWHvrzaDIFwxfoXzW4LVH+unHzT3om4T5SGYBPvVlFY3KzebU9/l78RwNIWBLCTw3c1xuQzj8U3rPJeiOXLQcTnBh644CYerUX1omAhrVVqwz+TkW8tRyLhdjqA/s227eee2GRwUXZ0DrSvc7O1c1ifWXm9btB6H6BF7lSDMyy/s1njYzxN+EtDUA8z4ZcOfeVGtlQsi5kBuwsWn5BrbrKo7TlMOPLvKYDsIgepYIXeEGx7lmbNpaEvwGMb91LhYS9sqcsPcDWGhZy74oQRup9hEe44YlDTZ75rDlrtwmObkQ0YvhApSa+sBmJbNH7fIf9NoGUXbAnbXPuJK7H/LwouxkoJukMldMWDss7SR9aL1F9WOaIzih9THQM5RIvrwbHpa8pM1OZP9Nh/ykkqw0JAwI1vPURDvOmeXoMPT9eYLkow5O0a05AyzfNMz0v2M5NSet0WrcxRkuL2k1m6IVn/hQyMz5jarxtAzAW30OsICzXQ0KazMrnc++o/YhNN1ckfhvIoD/KNudGnyJ4Bj1sYZ/jvfCYRfRNFKH+9boxq/+T46k/aSCxNkSNopYUtTuQm7v84YyTlTPi2H/UqjETfzRSs9Fko6R8S2JH3wgTf5wd1/PvO88HaR04ACp5zumX+bqZZ40+yWH/SQM40WtLVHD45EIUqYUpQ3pwdYO1Xkavrf+gu43DqIMq+bpLoZuqs9N0U8bTTdlqkjK50Vbkw/EARj89iFkza85UFcH4p3cuCVDjb63GmlvtfPM7jIzPTI/HEj328PIzFShWhaukDXsNZPcqsmp5t+3yqxJW1bJeWXiJ15hChoH4kB/TN8cTA25ehcnjnX0BcUYoou6roca3r2JMSqysHlNAHLwUZiyPLiq+8rO+K5GxsYHweeE6JxvhLVFMMddCevvjqXHsMMV2uMKshnLsnpHB5K2S7p3NqXgeyCA0Sgacv77kl6uIKuiMNIvzwoHRlgXeYx2mzmy+CCMCxcB5OIy0k9exorOpNYk/PAK7lT6Ib4onrfS7HIfvgcTJEHh3ZyTlpw41CpUNjjBMig4rcOXefpLlLs7Tn5+3UuNhKpvzs7D7rq6uxBocHEYuMBdmX2YoUZd9nFs5ysoc5Glq9HrNVscJBoP8EQOjnAn7/wuBFEAarYXYHqZVEVNtBinknbA4d3fW+cVSDCz3aV3pJvd9IQlX1SAg5Cu2QZxT6q2Qz24eWrby+wVX1wLTQ4A8slzRwdDQ+7iBQfUEq8a7QwADSa0rQs+dagy5Kvko1C93bCsxvxprfNwpbWWA4prWkMf5r83NdyufGRBkC+xGdFUKAlx0oLrXxeDAbNs8fn0FhX6sQvKxNVQPT9Kkx17NDxP4IyL6HZtSVaoHpFpvKPXFBaneoG7TjTH+mfdLVs2Ox9Pu1Ys+YeG0H7BLw/xa5NbdQn1t8eCjDbqLDI+xtJT/MOcUWI9fMwjsVZK/tjNeovFmAntt97P6EC+mlpajRpMcAuKPf1qDvDLTru9WdzYcTC3pKHxCDO6BLiUXGifyajineds1AiaAJxsf+kbB3xqJ5c/c3RGwykN798FwdqHwbasb8mSQuwqtVxfYbHEw2pvxzO1hQpnRCUxmCHDiKb+08F6RlwuDNjfPC9nMmSrksWNdTD/r/AYJHEPRbJuMGfrOZ51x1vkseqGHrK83RltEA1UV8hXuQLIcflrvs0BbS6TxVuqq5JDxJpe6PPzVq5TcBP3VT/1VAy5a0iKUBtX+NdE990XTTRDr7LmaGjQimyfUgOn5I5fDvjut6dFL9vO+zr2AKgwe7LdwA4Tl5A1J/8/kUOUiPG9A3tmaM5+z51hcnK11uET5r2sO3EQAhh/ql71jlX/VAOyZ79+DBEsdfVb4iMl1kxeLjKIG6k52s3nEPpNIMG3YcGSF7Z1rS2+BKOFnvt4+MyZ1uJag4JlFJDZGbowepa8yPAawuI+BrSapZe2yaa4YFtyem+2E2ST4xuzN/kFvWeV7MiSBidsXwe8uJF91zbz+oIb3wStLd+kTs3tVzY4cO5cgYTrkzlI6AM2lZTVLWXthxmQKqvLNm2zV109BBZEWrfU+IKroP+CO3r28D9Tt9qjU+7R56gGz5yD9XOVhKwx7G1R/RAOoDmsAjh5qO2mf6+Ac9NR2mGeF6hopMw14x56AXH2W4+yg5mh+JpJWiB7c0o9OIKuLPEbp8cLXIFrAHBxjmQmX6ybX73M6Ifnk9iJpaAm7+RAhj/EOPD08PXxlP0wAu4izwf7BUQ7+zTYCRJv9bRoqL6ssRr8pXHM9Kz79NKNfbnXrqEvUCcdzQhjMgEex3n6dLs5R4h+9gJLX43BJhDDAC1Rmyrmt1BUQbTo83Y3R0/zKHBtQ9NNbjRR/64vxPf3jvkeGXj62dtwwF1/Q6Y7lZf07oTSjfSaWsWouMkOSxB6N3GjNxXwucGSJbjcaXxmQhSWlxCQPeJFxIBAm/cOEMZgzNLjwB1UtnkLG3FY609LM/eDxhn2D1KGPDQFRgU3+QpTZjd2JVfExgE8k6EXO+BmJXCt3Wx7vaJOY3x+8c+vwAjulH8OI/rnJA6dp3oKIWHeVUPBC0JNeelwSWSlzfbjL+UrG40NUxyDPCNrrlsnOmypt2aOJjxghQe2jsbIBRQVdSeH5C+RYyAcnl7XfZ3q6UmF6QdrIoTsr5tqdDhOtVbqTa09PT9912sGGJh1h3b/OYQuKhw4gARvJI0+xA+wZYbctF/F701T209Ret0ylHvcWUfxP/391xa9xtYs1545C6/baxZdFdycX8aCEj5f92bFYfL86PqbigeTcEa2eYHP+GHGdwO0CLYszGY/U1NIlU493KDdoZ85ROs019JOAeZq/OGdbjhDPV7aNXMXjhCg0qBmX3xZUe7ur9w8BEgJMkKcey+t/+tBBFhz05z+lk6WzSk49bEEIq0aIC88OLoG6Fex1Kmfshi7B4bJlxJbLNBwBpnGz9NvAV/oo7PeN0tsfTdne4NLuHhpvRjRNhbSW2F9i+PcaIXBjAbnkPWUcWvf9CXccZWvtIRaeKp+33ivCMtfO7rGCbKlTjKQqE0cwrC8p4DLQB2nijy1twlim4zhjsKldQskcuduzhnnP1oTCDGWKVOnaAdO/9vSBVG1gonoezI6yXFRx8iNve8HeoNpu3vzVnmNCaZfGzi9x+PVlYHdpVum+xqDwYChY9xHPTENt7RBs+kXPsHt8GSx75PasvqqqHtRUoQL0TccZRNLfF7B+KFcct2b8atX8kOElcDDywvoQV6HTrU8YWYwPc3CoCeFc9bAzmp/cSLy0Q8sPOtjXhHT/BaLKvtj9rUvP6f66ksJluTc8M1EcWoqmNn6GxpT3WtsHCciCp/zgwSDebB5ERx4lVm3rO7jvsm+GvS+PwNYSyyQqq0CxwpcdTTFfPOKQsOsgi96gbRUtZDuCqDIDvC3fPGTeupDWlJlUHIr4+rEW05QP9PMhIySRkbqjAsSGGQNpGovItGK7wC7v2Ll/CSd9dHKVxtZTEZaUA2OapaOZX1z3GVCLWIO6L25Ho6/x/BdMGAx18zfZN0e6OVcDJfjTrJ+8BpDSGl4uBieWU/x0QCQufAV0UnX6uvKCY+VyL37dc053ns5R/16IKc2QfKYRoOVQfNC/Ft3pq87V2N+Qcnju9YKWG+oupObdqPix/j9SL7DsXYtYUEtu6b7O5V/A4mLOLlbOeUVLbzG9oNQ7IqppbeSy3elqYYGhqze4w7iMQRQKl4evnQh6NVBzYAZ2aJjuomkoThCcv98RjUMwS8dj1xc7eGzr4yyVLgK860KNf2VWvF849oL1+qPbZQfv3G0NFL5YuUy0hs3efjiCQaGQqmcyuI4v2WTZyw2XmhYVxzrf2q2GCfCRiklhpKkBnKS2LsFC8u7u2BFUybFms0LvQY6i+9XDVk7okTpIJ3VmoXvqtSvFN5+6W4jFsU3XTb1Dryr4LEY9LhYxah6YCIwf/tf+bfPItVSgRGppNepiSQq9oYHMd+nB2W05p7uHr1pGBGQHCyec8RHu8DTjrRjjW3CndBNKmvZMw2YKVn9v9m1SDSbEh4cAc+e+mIeUTQqKzEuUYc2pZFW37Ue/NM7baU97D448LRktXFknVLNYXIeDmzPH3KrikV9W0O6Q7tzrohIzRzM9bujZCc14clAQE4DuI4XL6jXfkWuA06l5AQEjv7C40V2oBJO6Pwa58N15TY6Fe+Us12mXxCAGs/LOSNXbyc/CLzRpVWuNtAtOlS35IE0XpuMzCEA89AjZ+YfLei6lu4mj0c6oDDjSRSGRBrIf7Bi2Eu+ps8S6jLM/zdrD5ZmBEbL+nnllQuFuWHrw1kLO69EMyXWMjZgAHy3bSXk7EQ/WU2KUkXT0aL+tJHBoPP54vcEQFXdpZDfGuR3DGRjl2fq0sSW12Fl6uh/9+nzFhJjFOyWR1S5AtTrxWQRSFlw1IAkYluBLTDfhgy4sVhTk7ByPgKrymkLWujTO/NVRIyUh6YXXnMN2N2/usYu6jmPrly3VdT3RxGSig3YPK018DgHc34eglKCenNScpIOndjfx8cFOcH8R94mwfUoZ5Q1yLgkz70Hj7Lr8cL++bHquqJ/llQae7yo++LV/Ur85xw8izsIwxwSg+3j1fJfy9JQ8seH4B85ttf67VWb0ypPEwTlHsns5roNQnHbfO+c70WFo5W1g79uWP2Lks39osFCXtpmnY5ApMZLL0wMMKfgMDaD2Ctnsb3XSvZV82g3+Pb1YSVJzjkXAq4EOOTSv6y6P39e1LiW/58gBHxE7WipX+aAPFgpsY8ma5x85h+cSheQSPovAylVXjwKui7R44PP6A9+KSpYE4l/DoCd+WlDQkhXfTF8aaloq6eiQJPlMew/0qk3HjJI70hwfP6vtIA/Q9D/ew0cc0TjUWk3btND1/PbS+05BtxRq6cb6FBaX3VsybVrXzaFk+lmNokOULfknNlmvdVC5MyjpzavzgIeHZUO/UIt9s4gpj8RoV+8yHr6P9TCBDRBn5DmedSoXMLqnTUTHd9tmTINMFllqNr7KFCoDzzWcHWCK/N7+64tsuJ0l7cE/l9n3l9juRVeYzVpmTrs7p0yX1vixBXGi8r0sBM2griLGOWPxjN0au23Zwa46eHvwsg5LlK/2XrqRjr3EEare+7b+zM3WVntLM+WMtuPK5LnlGmaPkR8/POh6mrYfn6EBGNQrquf1bzgVWcb79AsoLrCNgp45WKItmrnhS291qfz4ypXT1gTpqxOyd5Rg6Re9cl6H9K8Fhq1Ua5I22MtQECQ89hGHPIENkJzRo2mz61YKxXbsfvxroLqTA1vWkHXaQrsHFTSxhUaGotbshnI2D3YzKoKyynORl1L/I73rzN2v6hhTtZVD26VdLKz5TEuXFC//YPuMmAiWZ/I009R3XX7ELEVSNFKO7KSCocrasW3pBV8q0zBFFls7G7hkepnQJKvoOtJztNkgJVcAuySTmqyJL00IXr+OCay+rvhUDYDKbySoEFE+28JolvI95O3s3qJVoD62WTBXbC2X6dzqmhpmrZ8fbvd3k/UV1r1ds/uH7tuZnK2WCgy8BqFy277N7K8bnaFQ87BleO77m5/QBkha0IlJMIPjC4iUCQW2I+66qy1+IqVl5duj0Kg39zulNLw4TopaSCcdKqmcv9KwqwARSrK/NHHo5AgiChuaBcS2m+1DeEHSJ2oAIvcX7R++M6vl8djxkQPqeXzydo8y95a82rNpLhOj8Dav9p/yQ5N/mhY12BGdsmvopco1BgtNludDPBkOJxZBoq5GYcY1Yeds/vkrmqPUPo3A3FYvhm7hFxqSF7s9g5b4q+dnfcMN1S3o87y7Pn30/AOO/IVaq76SpaaaSex/eDgMw4d6ipwTejlmd89CzIZFjC/vsBVqRTE9vtRwZdNfW5/MMR6xBTj6DJLNfN/tc4HIMUO40W+kP3ZgYPaWSM0ES4uxl8kxStFN98wlBX6cfyKiMx6Cy9Vmag8Efl8wIFKAAubsQ2MNu+5yeDIbGBnkvNv4JGyAREbiviz94k3Dx8UqZ+K2gLxhsuGJfpaWgDNCAmNqX6Ip1N8hy0i5YItum8VcDC3PtQmje9hx8fBcpeBFBePPl2mvZzPnXomqDfEqWb4ZLlMmYCB5X8Ih9YfVG+PBpR30FPJyl2AbuLfbg1HoWCCWjEXYmQZaISPGr4QID/zA+zovLKpScPRUAu8LEr81yQ67Nml7PdqFvPKj8Ro3Rt9KmE7qJFUK1BPO07k5HlFg12ZDS0vFpZYWdEinMvILm2N5XGfQwMskt/EK910izBxTTDJryK0Obrbv3ViO7Oy9qD7m+yv9/aP8P5gpxJudrNvcYWxf/dvZqUkYIcGsIq2ti+MPXd63i56ZbeB5by88xDRnTi+a4WtJGwpx5MOTLu0KmtIYmluF6JJaWtzzMLeEf/AL962KgxyPCr/B+qMbTJZr2St9G7p1RKtr/vzGJyJKh4wmPQQkMhrPS7Ko5ajH6WWPb1fShnbs26rm5YpgTgEEX83AWY1Gw7rcZYXL5Csgj22qMELuYsgqDoYJ3c/mxI3VxKOdp8u3lG8OhtjTrdOvLvx5m+Kkh4BERq2DEca0lOXmO5OHZfAQK5/vY1yHppJgzmhsu5rRAuFfcKtTgzEJd+VJ/6Hjz2gANxnTTC9k48YN3KiuluhQW85CL/w96y+xFxu+vSCXgSkQICuqOHZwucttvsnI+DBDJmj7kRF4eVXnR2si1C1eiB7dgd7cufArzD17dknVyeJi4vc4oRUsxFZwxoo4oPFEz8UW1MbI+VIXlHOblR2kSk+FgKZWa0c4aPcUf05Ov4rzO7y6y72glcmJb4QEBRXHliRMY9iuyycsl6tBvjeCIYPxAhpnNapj9eMUBVardYmKknT9PDFPdKyeC/j/0fIkbQD2Ao/mWCFRw80jo5GsbA6HL937akFmdI3Pnf+4+Jzj9RKU0+TGmfydZAOxg/Ek9g1lmmrcyW/24njTS1zcSkGnN4po3XA0nYIGEBjOxRCOx+nDyjjEjJIzmXZXdeFeE1r41hKjmdLox29OPjh2Ftk1QUZkGDgrAWVeXYasOLK5bgqYcREeERFogADUpAMwFQKQ4iZpYc4bDcabBcKdAjJ4WBLNd6lWte3+s5SsaUurcBIXzmpvSjQmZQto7ODkrTT0ZHqkvVXXp3srs9M8VlZLA8GhFdyJb0KRiQOS35Ai/nV+uBUeG+at1y/RuHbBawUp2JQu/kuZOBAMp3ZW5S65S6PVrk0yQjiZjbXJ/Hk2DdGiaN8WgD5SCnAdbIBW29Q0AKn8FaQQ4vJyONwRDy8uWEF2jT/Gi72Pqgh/o4b1cO2K7cZGsKJpVCKqOEJXzCcvJDLOPmr9zkXNQvR61EVr5kxNA2AQym8HYhlNDQJnlekdQniNR6n0e49vuEB4vwaW1bzCoF042c/kkuz3tlyxoB8PtZtFmYgq4jn01yDOeooaSKwFTcBKw7TcqUxY4MvMEQGsQ+0HZBUZ+PYwYsYyoWCaE3JDsZisTTfTtA9bbkm9tHKR4bgINf/Nzn2y05AgJ7dGO54DrH2ov1A1mK9dgSa/Dx5gMVDhL0MTg3EFMV2ihG3AmlItLT7fez+nFy3kvce/lGreOFUCSLgC7WysKF+6XNLIsJ0sefU/fFjYYIQChWHWwdJsHzf4JMZjDUqb1vfsdth+QDWDLQLhwW8ITN4GSGRE/ro5OvPFYorUyEyuUKz9iHCKPhVlcB5+LEO2JygKTUykkuVMJrnYrM1JuxqWIr/JTUUDEFfoBjhYuWiQ5uZHWZNY4ATS4nwlxmir8XHzcWuTQwxn1zy5m4UtW67PkN1VteZN8WBKR7ulujlI0V5h/JZkABUbPn5gJVRIrbHIU3lJDdFBCjQWNdNYnYRYTV7tREBYVqfCG0c4NQ1AXAKUTJlO7iJpGgXUsQlycGVJnfOg6andVrOik9tAR0m45pmcGX+afOuKeAFS+X/z9NQOt0uBA92ghRvSUEZVmzeBtGI4TiUP9ZEuOpHv5rJ/e+f0g+v4t0TM4LeqaHn+JuExRQIM0sQIimBM5pYbwic8MiS154p0hGFr0QpubhmM5Apu1jRwQb/OcLCOvHH3b2nkqcwC4oz06cZf3tQgr04oniKel2i8Yoi/FTXSYIhN8nt066eokoAMA1LHgO9v32lSQekfEHmRT6FR9pgy0Iw6BOZOKJ5Wf/CeTDFYS1bUTb+9Zv6Tzd86yhU0N5SnNvfOa3+zUZuaERJndI5UX6lq/MDxCbOwBLsS89T15g6Gr4BYjbQVialIHCZQQdTe+LsFT/UXFjfUXp+i0R83yEmg1viJsR0g0ddS1C2QAQFtAQHiQeUap3v/PoOmSiBQn/K6Ao3WvZMRJ8FsIv+TIjJ1zCjjv6Mgpeb4tqxYBpHfq/dTHoJxH5+jSQ1Mn9QIEM9hDJq4HLkZpS+DIpVDSOudgyxTmwUYt+zxq1/jpEZgfKDW82Nc3fRoUjsu7pFh+k75dsoEIEXlIeuMtMjkpPdu467zflO4ZmhpHE/7vYMpDwHw3GtPun616SSl9ZcVQXr99Gkfihw+gYCBaZUcElImLY53MgJ/xtSHAAy0xkZphkmLp7xH72/hEzRAVrmiiRbCyeMTNIAc/U4b/F34lN+aubDe//t+/vIpQ/C34t+f+/1L4F8C/xL4l8C/BP4l8H/JPRIlghZiyQAAAABJRU5ErkJggg==";
        #endregion

        #region [1-5. 커서 설정 관련 상태 변수]
        /// <summary>현재 적용된 커서의 Base64 문자열</summary>
        private string CursorBase64 = PRESET_DEFAULT;
        /// <summary>커서 크기 값</summary>
        private int CursorSize;
        /// <summary>커서 투명도 값 (0 ~ 255)</summary>
        private byte CursorOpacity;
        #endregion

        #region [1-6. 스킬 범위(Skill Shape) 관련 상태 및 설정 변수]
        /// <summary>자기 중심 범위 오버레이 활성화 여부</summary>
        private bool SkillEnable_UserRange;
        /// <summary>직선 범위 오버레이 활성화 여부</summary>
        private bool SkillEnable_StraightRange;
        /// <summary>지정 범위 오버레이 활성화 여부</summary>
        private bool SkillEnable_CustomRange;
        /// <summary>범위 연마 효과 적용 여부</summary>
        private bool Skill_RangeExtension;
        /// <summary>불시엔 아이템 효과 적용 여부</summary>
        private bool Skill_RangeExtensionItem;
        /// <summary>기준 스킬 범위 값</summary>
        private int Skill_Range;
        /// <summary>계산된 스킬 범위 오버레이 너비</summary>
        private int Skill_RangeWidth;
        /// <summary>계산된 스킬 범위 오버레이 높이</summary>
        private int Skill_RangeHeight;
        /// <summary>스킬 범위 오버레이 투명도</summary>
        private byte Skill_RangeOpacity;
        /// <summary>스킬 범위 최소 크기 제한</summary>
        private int Skill_Range_Min;
        /// <summary>스킬 범위 최대 크기 제한</summary>
        private int Skill_Range_Max;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_SHOWNOACTIVATE = 4;
        #endregion

        #endregion

        #region [2. 생성자 및 초기화 기능 (Constructor & Lifecycle)]

        /// <summary>
        /// Form1 클래스의 새 인스턴스를 초기화하고 GDI 폼 및 기본 설정값을 세팅합니다.
        /// </summary>
        public Form1()
        {
            // GDI 오버레이 폼 인스턴스 생성
            this.mCursorForm = new GdiForm();
            this.mClickCursorForm = new GdiForm();
            this.mSkillForm = new GdiForm();

            // 기본 커서 속성 초기화
            this.CursorSize = 40;
            this.CursorOpacity = byte.MaxValue;

            // 기본 스킬 범위 속성 초기화
            this.SkillEnable_UserRange = false;
            this.SkillEnable_StraightRange = false;
            this.SkillEnable_CustomRange = false;
            this.Skill_Range = 44;
            this.Skill_RangeHeight = this.Skill_Range * 10;
            this.Skill_RangeWidth = this.Skill_RangeHeight * 2;
            this.Skill_RangeOpacity = 100;
            this.Skill_Range_Min = 300;
            this.Skill_Range_Max = 1280;

            // 디자이너 컴포넌트 초기화
            this.InitializeComponent();
        }

        /// <summary>
        /// 폼이 로드될 때 마우스 후크를 등록하고 저장된 설정을 불러와 UI에 반영합니다.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                IntPtr hSkill = this.mSkillForm.Handle;

                // 마우스 이벤트 후크 등록
                MouseHook.MouseMove += this.MouseHook_MouseMove;
                if (!MouseHook.HookStart())
                {
                    MessageBox.Show("Mouse hook failed");
                }

                // 설정 파일 로드
                this.LoadSettings();

                // UI 컨트롤 상태 동기화
                if (this.textBox_Mouse_Size != null) this.textBox_Mouse_Size.Text = this.CursorSize.ToString();
                if (this.Mouse_Opacity != null) this.Mouse_Opacity.Value = this.CursorOpacity;
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.Text = this.CursorBase64;
                if (this.textBox_Skill_Range != null) this.textBox_Skill_Range.Text = this.Skill_Range.ToString();

                if (this.checkBox_Skill_RangeExtension != null)
                {
                    this.checkBox_Skill_RangeExtension.Checked = this.Skill_RangeExtension;
                }
                if (this.checkBox_Skill_RangeExtensionItem != null)
                {
                    this.checkBox_Skill_RangeExtensionItem.Checked = this.Skill_RangeExtensionItem;
                }

                if (this.radioButton_Skill_UserRange != null)
                {
                    this.radioButton_Skill_UserRange.Checked = this.SkillEnable_UserRange;
                }
                if (this.radioButton_Skill_StraightRange != null)
                {
                    this.radioButton_Skill_StraightRange.Checked = this.SkillEnable_StraightRange;
                }
                if (this.radioButton_Skill_CustomRange != null)
                {
                    this.radioButton_Skill_CustomRange.Checked = this.SkillEnable_CustomRange;
                }
                if (this.radioButton_Skill_None != null && !this.SkillEnable_UserRange && !this.SkillEnable_StraightRange && !this.SkillEnable_CustomRange)
                {
                    this.radioButton_Skill_None.Checked = true;
                }

                if (this.Skill_Range_Opacity != null) this.Skill_Range_Opacity.Value = this.Skill_RangeOpacity;

                // 프리셋 및 커서/스킬 형상 초기화 적용
                this.UpdatePresetSelection();
                this.SetCursor();
                this.mCursorForm.Show();
                this.mIsLoadForm = true;
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initialization failed. - " + ex.Message);
            }
        }

        /// <summary>
        /// 폼이 화면에 완전히 표시될 때 초기 상태를 한 번 더 동기화합니다.
        /// </summary>
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!this.mIsLoadForm)
            {
                if (this.textBox_Mouse_Size != null) this.textBox_Mouse_Size.Text = this.CursorSize.ToString();
                if (this.Mouse_Opacity != null) this.Mouse_Opacity.Value = this.CursorOpacity;
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.Text = this.CursorBase64;
                if (this.textBox_Skill_Range != null) this.textBox_Skill_Range.Text = this.Skill_Range.ToString();
                this.UpdatePresetSelection();
                this.SetCursor();
                this.mCursorForm.Show();
                this.mClickCursorForm.Hide();
            }
            this.mIsLoadForm = true;

            this.Activate();
            this.Focus();
        }

        /// <summary>
        /// 폼이 닫힐 때 마우스 후크를 해제하고 트레이 아이콘 및 오버레이 폼을 정리합니다.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.notifyIcon1 != null)
                {
                    this.notifyIcon1.Visible = false;
                }
                // 후크 해제 및 이벤트 구독 제거
                MouseHook.HookClose();
                MouseHook.MouseDown -= this.MouseHook_MouseDown;
                MouseHook.MouseMove -= this.MouseHook_MouseMove;

                // 오버레이 폼 숨기기
                this.mCursorForm.Hide();
                if (this.mSkillForm != null)
                {
                    this.mSkillForm.Hide();
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region [3. 마우스 후크(Hook) 및 이벤트 처리 기능]

        /// <summary>
        /// 마우스 커서 이동 시 호출되며, 커서 및 지정 범위 오버레이 폼의 위치를 실시간으로 업데이트합니다.
        /// </summary>
        private bool MouseHook_MouseMove(MouseEventType type, int x, int y)
        {
            try
            {
                // 1. 마우스 커서 위치 업데이트
                this.mCursorForm.Left = x - 10;
                this.mCursorForm.Top = y - 10;

                // 2. 지정 범위 모드일 경우 마우스 커서와 함께 붙어서 움직이도록 위치 실시간 연동
                if (this.SkillEnable_CustomRange && this.mSkillForm != null && Form1.mBitmap_Skill != null)
                {
                    int width = Form1.mBitmap_Skill.Width;
                    int height = Form1.mBitmap_Skill.Height;
                    this.mSkillForm.Left = x - width / 2;
                    this.mSkillForm.Top = y - height / 2;
                }
            }
            catch (Exception)
            {
            }
            return true;
        }

        /// <summary>
        /// 마우스 클릭 발생 시 호출되며, 클릭 위치에 클릭 효과 오버레이를 표시합니다.
        /// </summary>
        private bool MouseHook_MouseDown(MouseEventType type, int x, int y)
        {
            try
            {
                this.mClickCursorForm.Left = x - Form1.mBitmap_Click.Width / 2;
                this.mClickCursorForm.Top = y - Form1.mBitmap_Click.Height / 2;
                this.mClickCursorForm.Show();
            }
            catch (Exception)
            {
            }
            return true;
        }

        /// <summary>
        /// 투명도 트랙바 스크롤 시 호출되어 커서의 투명도 값을 실시간으로 조절합니다.
        /// </summary>
        private void Mouse_Opacity_Scroll(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TrackBar trackBar = sender as System.Windows.Forms.TrackBar;
                if (trackBar != null)
                {
                    int opacityValue = trackBar.Value;

                    this.CursorOpacity = (byte)opacityValue;
                    this.SetCursor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("투명도 조절 중 오류 발생: " + ex.Message);
            }
        }

        #endregion

        #region [4. 설정 적용 및 관리 기능 (Save / Apply)]

        /// <summary>
        /// 사용자가 입력한 설정값(크기, 투명도, 스킬 범위 등)의 유효성을 검증하고 적용합니다.
        /// </summary>
        private void SettingApply_Save()
        {
            try
            {
                int CursorSize = 0;
                if (this.textBox_Mouse_Size != null && !int.TryParse(this.textBox_Mouse_Size.Text, out CursorSize) || CursorSize == 0)
                {
                    MessageBox.Show("숫자만 입력 가능합니다.");
                    CursorSize = 40;
                    this.textBox_Mouse_Size.Text = CursorSize.ToString();
                }
                this.CursorSize = CursorSize;

                byte CursorOpacity = 0;
                if (this.Mouse_Opacity != null && !byte.TryParse(this.Mouse_Opacity.Value.ToString(), out CursorOpacity))
                {
                    MessageBox.Show("0부터 255까지의 숫자만 입력 가능합니다.");
                    CursorOpacity = byte.MaxValue;
                    this.Mouse_Opacity.Value = CursorOpacity;
                }
                this.CursorOpacity = CursorOpacity;

                this.SetCursor();

                int Skill_Range = 0;
                bool isAnySkillActive = this.SkillEnable_UserRange || this.SkillEnable_StraightRange || this.SkillEnable_CustomRange;
                if (isAnySkillActive && this.textBox_Skill_Range != null && !int.TryParse(this.textBox_Skill_Range.Text, out Skill_Range))
                {
                    MessageBox.Show("숫자만 입력 가능합니다.");
                    Skill_Range = 44;
                    this.textBox_Skill_Range.Text = Skill_Range.ToString();
                }
                if (!int.TryParse(this.textBox_Skill_Range.Text, out Skill_Range) || Skill_Range < 30 || Skill_Range > 60)
                {
                    if (Skill_Range < 30)
                    {
                        MessageBox.Show("30 ~ 60 숫자만 입력 가능합니다.");
                        Skill_Range = 30;
                    }
                    else if (Skill_Range > 60)
                    {
                        MessageBox.Show("30 ~ 60 숫자만 입력 가능합니다.");
                        Skill_Range = 60;
                    }
                        this.textBox_Skill_Range.Text = Skill_Range.ToString();
                }
                this.Skill_Range = Skill_Range;

                this.SetSkillShape();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Base64 문자열을 디코딩하여 마우스 커서 비트맵을 생성하고 오버레이 폼에 설정합니다.
        /// </summary>
        private void SetCursor()
        {
            try
            {
                string s = string.IsNullOrEmpty(this.CursorBase64) ? PRESET_DEFAULT : this.CursorBase64.Replace("\r", "").Replace("\n", "").Trim();
                byte[] buffer;
                try
                {
                    buffer = Convert.FromBase64String(s);
                }
                catch
                {
                    s = PRESET_DEFAULT;
                    this.CursorBase64 = s;
                    buffer = Convert.FromBase64String(s);
                }
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (Bitmap bitmap = new Bitmap(memoryStream))
                    {
                        int num = this.CursorSize * 2;
                        Form1.mBitmap_Mouse = new Bitmap(bitmap, new Size(num, num));
                    }
                }
                this.mCursorForm.SetBitmap(Form1.mBitmap_Mouse, this.CursorOpacity);
            }
            catch (Exception ex)
            {
                MessageBox.Show("커서 이미지 디코딩에 실패했습니다: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        #endregion

        #region [5. 저장된 설정값 로드 및 저장 (Load / Save Settings)]

        /// <summary>
        /// INI 파일로부터 저장된 설정값들을 읽어와 필드에 반영합니다.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                if (File.Exists(this.configPath))
                {
                    string[] array = File.ReadAllLines(this.configPath);
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text = array[i].Trim();
                        if (!string.IsNullOrEmpty(text))
                        {
                            int num = text.IndexOf('=');
                            if (num > 0)
                            {
                                string a = text.Substring(0, num).Trim();
                                string text2 = text.Substring(num + 1).Trim();
                                int CursorSize;
                                byte CursorOpacity;
                                bool flag;
                                int Skill_Range;
                                byte Skill_RangeOpacity;

                                if (a == "Size" && int.TryParse(text2, out CursorSize))
                                {
                                    this.CursorSize = CursorSize;
                                }
                                else if (a == "Opacity" && byte.TryParse(text2, out CursorOpacity))
                                {
                                    this.CursorOpacity = CursorOpacity;
                                }
                                else if (a == "Cursor" && !string.IsNullOrEmpty(text2))
                                {
                                    this.CursorBase64 = text2.Replace("\r", "").Replace("\n", "").Trim();
                                }
                                else if (a == "SkillEnable_UserRange" && bool.TryParse(text2, out flag))
                                {
                                    this.SkillEnable_UserRange = flag;
                                }
                                else if (a == "SkillEnable_StraightRange" && bool.TryParse(text2, out flag))
                                {
                                    this.SkillEnable_StraightRange = flag;
                                }
                                else if (a == "SkillEnable_CustomRange" && bool.TryParse(text2, out flag))
                                {
                                    this.SkillEnable_CustomRange = flag;
                                }
                                else if (a == "Skill_RangeExtension" && bool.TryParse(text2, out flag))
                                {
                                    this.Skill_RangeExtension = flag;
                                    if (this.checkBox_Skill_RangeExtension != null) this.checkBox_Skill_RangeExtension.Checked = flag;
                                }
                                else if (a == "Skill_RangeExtensionItem" && bool.TryParse(text2, out flag))
                                {
                                    this.Skill_RangeExtensionItem = flag;
                                    if (this.checkBox_Skill_RangeExtensionItem != null) this.checkBox_Skill_RangeExtensionItem.Checked = flag;
                                }
                                else if (a == "Skill_Range" && int.TryParse(text2, out Skill_Range))
                                {
                                    this.Skill_Range = Skill_Range;
                                }
                                else if (a == "Skill_RangeOpacity" && byte.TryParse(text2, out Skill_RangeOpacity))
                                {
                                    this.Skill_RangeOpacity = Skill_RangeOpacity;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            // 기본값 보정
            if (this.CursorSize <= 0) { this.CursorSize = 20; }
            if (this.CursorOpacity == 0) { this.CursorOpacity = byte.MaxValue; }
            if (string.IsNullOrEmpty(this.CursorBase64)) { this.CursorBase64 = PRESET_DEFAULT; }
            if (this.Skill_Range <= 0) { this.Skill_Range = 44; }
            if (this.Skill_RangeOpacity <= 0) { this.Skill_RangeOpacity = byte.MaxValue; }

            int calcHeight = this.Skill_Range * 10;
            if (calcHeight < this.Skill_Range_Min) { calcHeight = this.Skill_Range_Min; }
            if (calcHeight > this.Skill_Range_Max) { calcHeight = this.Skill_Range_Max; }
            this.Skill_RangeHeight = calcHeight;
            this.Skill_RangeWidth = calcHeight * 2;
        }

        /// <summary>
        /// 현재 설정된 값들을 INI 설정 파일에 저장합니다.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                string text = string.IsNullOrEmpty(this.CursorBase64) ? "" : this.CursorBase64.Replace("\r", "").Replace("\n", "").Trim();
                string contents = string.Format("Size={0}\nOpacity={1}\nCursor={2}\nSkillEnable_UserRange={3}\nSkillEnable_StraightRange={4}\nSkillEnable_CustomRange={5}\nSkill_RangeExtension={6}\nSkill_RangeExtensionItem={7}\nSkill_Range={8}\nSkill_RangeOpacity={9}", new object[]
                {
                    this.CursorSize,
                    this.CursorOpacity,
                    text,
                    this.SkillEnable_UserRange,
                    this.SkillEnable_StraightRange,
                    this.SkillEnable_CustomRange,
                    this.Skill_RangeExtension,
                    this.Skill_RangeExtensionItem,
                    this.Skill_Range,
                    this.Skill_RangeOpacity
                });
                File.WriteAllText(this.configPath, contents);
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region [6. 이벤트 핸들러 (Event Handlers)]

        /// <summary>
        /// 텍스트박스에서 Enter 키 입력 시 설정을 적용합니다.
        /// </summary>
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.SettingApply_Save();
            }
        }

        #endregion

        #region [7. UI 제어 (UI Handlers)]

        /// <summary>
        /// [설정값 저장] 버튼 클릭 시 설정을 적용하고 파일에 저장합니다.
        /// </summary>
        private void button_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                this.SettingApply_Save();
                MessageBox.Show("설정값이 저장되었습니다.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.SaveSettings();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// [트레이로 최소화] 버튼 클릭 시 창을 숨깁니다.
        /// </summary>
        private void button_MinimizeTray_Click(object sender, EventArgs e)
        {
            base.Hide();
        }

        /// <summary>
        /// ESC 버튼 클릭 시 창을 숨깁니다.
        /// </summary>
        private void button_MinimizeTray_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                // button_MinimizeTray 클릭 이벤트 메서드를 직접 호출하거나 수행
                button_MinimizeTray_Click(sender, e);

                // 키 입력이 다른 컨트롤로 전파되지 않도록 처리
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// 트레이 메뉴 [열기] 클릭 시 창을 복원합니다.
        /// </summary>
        private void TrayMenu_Open_Click(object sender, EventArgs e)
        {
            base.Show();
            base.WindowState = FormWindowState.Normal;
            base.BringToFront();
        }

        /// <summary>
        /// 트레이 메뉴 [종료] 클릭 시 애플리케이션을 종료합니다.
        /// </summary>
        private void TrayMenu_Exit_Click(object sender, EventArgs e)
        {
            if (this.notifyIcon1 != null)
            {
                this.notifyIcon1.Visible = false;
            }
            Application.Exit();
        }

        /// <summary>
        /// 시스템 트레이 아이콘 더블클릭 시 창을 복원합니다.
        /// </summary>
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            base.Show();
            base.WindowState = FormWindowState.Normal;
            base.BringToFront();
        }

        /// <summary>
        /// [커서 변경] 버튼 클릭 시 입력된 Base64 코드를 검증하고 커서를 교체합니다.
        /// </summary>
        private void button_ChangeCursor_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox_CursorBase64 == null) return;
                string text = this.textBox_CursorBase64.Text.Replace("\r", "").Replace("\n", "").Trim();
                if (string.IsNullOrEmpty(text))
                {
                    MessageBox.Show("Base64 코드를 입력해주세요.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text)))
                    {
                        using (new Bitmap(memoryStream))
                        {
                        }
                    }
                    this.CursorBase64 = text;
                    this.textBox_CursorBase64.Text = text;
                    this.SetCursor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("유효하지 않은 Base64 이미지 코드입니다: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// Base64 변환 웹사이트 링크 클릭 시 기본 브라우저로 해당 페이지를 엽니다.
        /// </summary>
        private void linkLabel_CursorUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://www.base64.sh/ko/image-to-base64/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("링크를 열 수 없습니다: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// 커서 프리셋 콤보박스 선택 변경 시 해당 프리셋 데이터를 로드하고 텍스트박스 상태를 제어합니다.
        /// </summary>
        private void comboBox_CursorPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.mIsLoadForm || this.comboBox_CursorPreset == null)
            {
                return;
            }
            object selectedItem = this.comboBox_CursorPreset.SelectedItem;
            string a = (selectedItem != null) ? selectedItem.ToString() : null;
            if (a == "기본 커서 (Default)")
            {
                this.CursorBase64 = PRESET_DEFAULT;
                if (this.textBox_CursorBase64 != null)
                {
                    this.textBox_CursorBase64.Text = PRESET_DEFAULT;
                    this.textBox_CursorBase64.ReadOnly = true;
                }
                this.SetCursor();
                return;
            }
            if (a == "블루 커서 (Blue)")
            {
                this.CursorBase64 = PRESET_BLUE;
                if (this.textBox_CursorBase64 != null)
                {
                    this.textBox_CursorBase64.Text = PRESET_BLUE;
                    this.textBox_CursorBase64.ReadOnly = true;
                }
                this.SetCursor();
                return;
            }
            if (a == "레드 커서 (Red)")
            {
                this.CursorBase64 = PRESET_RED;
                if (this.textBox_CursorBase64 != null)
                {
                    this.textBox_CursorBase64.Text = PRESET_RED;
                    this.textBox_CursorBase64.ReadOnly = true;
                }
                this.SetCursor();
                return;
            }
            if (a == "직접 입력 (Custom)" && this.textBox_CursorBase64 != null)
            {
                this.textBox_CursorBase64.ReadOnly = false;
            }
        }

        /// <summary>
        /// 현재 설정된 커서 데이터에 맞춰 프리셋 콤보박스 선택 상태를 동기화합니다.
        /// </summary>
        private void UpdatePresetSelection()
        {
            if (this.comboBox_CursorPreset == null) return;

            if (this.CursorBase64 == PRESET_DEFAULT)
            {
                this.comboBox_CursorPreset.SelectedItem = "기본 커서 (Default)";
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.ReadOnly = true;
                return;
            }
            if (this.CursorBase64 == PRESET_BLUE)
            {
                this.comboBox_CursorPreset.SelectedItem = "블루 커서 (Blue)";
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.ReadOnly = true;
                return;
            }
            if (this.CursorBase64 == PRESET_RED)
            {
                this.comboBox_CursorPreset.SelectedItem = "레드 커서 (Red)";
                if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.ReadOnly = true;
                return;
            }
            this.comboBox_CursorPreset.SelectedItem = "직접 입력 (Custom)";
            if (this.textBox_CursorBase64 != null) this.textBox_CursorBase64.ReadOnly = false;
        }

        #endregion

        #region [8. 스킬 범위(Skill Shape) 관련 기능]

        /// <summary>
        /// 스킬 범위 설정 및 연마/아이템 옵션에 따라 표시될 스킬 범위 오버레이 모양을 생성합니다.
        /// (자기 중심: 마름모, 직선 범위: 직사각형, 지정 범위: 정원형 - 마우스 커서 연동)
        /// </summary>
        private void SetSkillShape()
        {
            try
            {
                bool isAnyEnabled = this.SkillEnable_UserRange || this.SkillEnable_StraightRange || this.SkillEnable_CustomRange;
                if (!isAnyEnabled)
                {
                    if (this.mSkillForm != null)
                    {
                        this.mSkillForm.Hide();
                    }
                }
                else
                {
                    // 구분을 위한 모드별 색상 값 지정 (자기 중심: Cyan, 직선 범위: Orange, 지정 범위: Red)
                    Color activeColor = Color.Cyan;
                    if (this.SkillEnable_StraightRange)
                    {
                        activeColor = Color.Orange;
                    }
                    else if (this.SkillEnable_CustomRange)
                    {
                        activeColor = Color.Red;
                    }

                    int effectiveRange = this.Skill_Range;

                    bool isExtChecked = (this.checkBox_Skill_RangeExtension != null && this.checkBox_Skill_RangeExtension.Checked);
                    bool isItemChecked = (this.checkBox_Skill_RangeExtensionItem != null && this.checkBox_Skill_RangeExtensionItem.Checked);

                    // 직선 범위(SkillEnable_StraightRange)일 경우 크기를 1.7배로 확대
                    if (this.SkillEnable_StraightRange)
                    {
                        effectiveRange = (int)(effectiveRange * 1.7);
                    }

                    if (isExtChecked)
                    {
                        effectiveRange += 20; // 범위 연마
                    }
                    if (isItemChecked)
                    {
                        effectiveRange += 7; // 불시엔
                    }

                    int num = effectiveRange * 10;

                    if (isExtChecked || isItemChecked && !this.SkillEnable_StraightRange)
                    {
                        num += 100; // 추가 범위 보정
                    }

                    num = Math.Max(this.Skill_Range_Min, Math.Min(this.Skill_Range_Max, num));

                    int shapeWidth = num * 2;
                    int shapeHeight = num;

                    this.Skill_RangeWidth = shapeWidth;

                    Bitmap image = new Bitmap(shapeWidth + 4, shapeHeight + 4);
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(100, activeColor)))
                        using (Pen pen = new Pen(activeColor, 2f))
                        {
                            if (this.SkillEnable_UserRange)
                            {
                                // 1. 자기 중심 범위: 마름모 형태
                                PointF pointF = new PointF((float)shapeWidth / 2f + 2f, 2f);
                                PointF pointF2 = new PointF((float)(shapeWidth + 2), (float)shapeHeight / 2f + 2f);
                                PointF pointF3 = new PointF((float)shapeWidth / 2f + 2f, (float)(shapeHeight + 2));
                                PointF pointF4 = new PointF(2f, (float)shapeHeight / 2f + 2f);
                                PointF[] points = new PointF[]
                                {
                                    pointF,
                                    pointF2,
                                    pointF3,
                                    pointF4
                                };
                                graphics.FillPolygon(solidBrush, points);
                                graphics.DrawPolygon(pen, points);
                            }
                            else if (this.SkillEnable_StraightRange)
                            {
                                // 2. 직선 범위: 정원형 형태
                                graphics.FillEllipse(solidBrush, 2f, 2f, (float)shapeWidth, (float)shapeHeight);
                                graphics.DrawEllipse(pen, 2f, 2f, (float)shapeWidth, (float)shapeHeight);
                            }
                            else if (this.SkillEnable_CustomRange)
                            {
                                // 3. 지정 범위: 정원형 형태
                                graphics.FillEllipse(solidBrush, 2f, 2f, (float)shapeWidth, (float)shapeHeight);
                                graphics.DrawEllipse(pen, 2f, 2f, (float)shapeWidth, (float)shapeHeight);
                            }
                        }
                    }
                    Form1.mBitmap_Skill = image;

                    // 자기 중심 및 직선 범위는 화면 중앙에 고정, 지정 범위는 현재 마우스 위치(커서)를 기준으로 즉시 배치
                    if (this.SkillEnable_CustomGroupActiveCheckMethodOrCursor()) // 내부 처리용 분기 예시
                    {
                        // 마우스 실시간 위치에 맞추거나 초기 표시 시 화면 중앙 정렬
                    }

                    Rectangle bounds = Screen.PrimaryScreen.Bounds;
                    int left = (bounds.Width - (shapeWidth + 4)) / 2;
                    int top = (bounds.Height - (shapeHeight + 4)) / 2 - 30;

                    this.mSkillForm.Left = left;
                    this.mSkillForm.Top = top;
                    this.mSkillForm.SetBitmap(Form1.mBitmap_Skill, this.Skill_RangeOpacity);
                    //this.mSkillForm.Show();
                    ShowWindow(this.mSkillForm.Handle, SW_SHOWNOACTIVATE);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("범위 생성 실패: " + ex.Message);
            }
        }

        // 보조 메서드 (지정 범위 여부 확인용)
        private bool SkillEnable_CustomGroupActiveCheckMethodOrCursor()
        {
            return this.SkillEnable_CustomRange;
        }

        /// <summary>
        /// '[사용안함]' 라디오 버튼 상태 변경 시 호출되어 모든 스킬 범위 오버레이를 끕니다.
        /// </summary>
        private void radioButton_Skill_None_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton_Skill_None != null && this.radioButton_Skill_None.Checked)
                {
                    this.SkillEnable_UserRange = false;
                    this.SkillEnable_StraightRange = false;
                    this.SkillEnable_CustomRange = false;
                    this.SetSkillShape();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 적용 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '[자기 중심 범위]' 체크박스(라디오버튼) 상태 변경 시 호출되어 스킬 범위 오버레이를 켜고 끕니다.
        /// </summary>
        private void radioButton_SkillEnable_UserRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton_Skill_UserRange != null)
                {
                    this.SkillEnable_UserRange = this.radioButton_Skill_UserRange.Checked;
                }
                if (this.SkillEnable_UserRange)
                {
                    this.SkillEnable_StraightRange = false;
                    this.SkillEnable_CustomRange = false;
                }
                int num;
                if (this.textBox_Skill_Range != null && int.TryParse(this.textBox_Skill_Range.Text, out num))
                {
                    int num2 = num * 10;
                    if (num2 < this.Skill_Range_Min) { num2 = this.Skill_Range_Min; }
                    if (num2 > this.Skill_Range_Max) { num2 = this.Skill_Range_Max; }
                    this.Skill_RangeHeight = num2;
                    this.Skill_RangeWidth = num2 * 2;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 적용 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '[직선 범위]' 라디오 버튼 상태 변경 시 호출되어 직선 범위 오버레이를 켜고 끕니다.
        /// </summary>
        private void radioButton_Skill_StraightRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton_Skill_StraightRange != null)
                {
                    this.SkillEnable_StraightRange = this.radioButton_Skill_StraightRange.Checked;
                }
                if (this.SkillEnable_StraightRange)
                {
                    this.SkillEnable_UserRange = false;
                    this.SkillEnable_CustomRange = false;
                }
                int num;
                if (this.textBox_Skill_Range != null && int.TryParse(this.textBox_Skill_Range.Text, out num))
                {
                    int num2 = num * 10;
                    if (num2 < this.Skill_Range_Min) { num2 = this.Skill_Range_Min; }
                    if (num2 > this.Skill_Range_Max) { num2 = this.Skill_Range_Max; }
                    this.Skill_RangeHeight = num2;
                    this.Skill_RangeWidth = num2 * 2;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 적용 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '[지정 범위]' 라디오 버튼 상태 변경 시 호출되어 지정 범위 오버레이를 켜고 끕니다.
        /// </summary>
        private void radioButton_Skill_CustomRange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radioButton_Skill_CustomRange != null)
                {
                    this.SkillEnable_CustomRange = this.radioButton_Skill_CustomRange.Checked;
                }
                if (this.SkillEnable_CustomRange)
                {
                    this.SkillEnable_UserRange = false;
                    this.SkillEnable_StraightRange = false;
                }
                int num;
                if (this.textBox_Skill_Range != null && int.TryParse(this.textBox_Skill_Range.Text, out num))
                {
                    int num2 = num * 10;
                    if (num2 < this.Skill_Range_Min) { num2 = this.Skill_Range_Min; }
                    if (num2 > this.Skill_Range_Max) { num2 = this.Skill_Range_Max; }
                    this.Skill_RangeHeight = num2;
                    this.Skill_RangeWidth = num2 * 2;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("설정 적용 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '범위 연마' 체크박스 상태 변경 시 스킬 범위를 재계산하고 오버레이를 갱신합니다.
        /// </summary>
        private void checkBox_Skill_RangeExtension_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.checkBox_Skill_RangeExtension != null)
                {
                    this.Skill_RangeExtension = this.checkBox_Skill_RangeExtension.Checked;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("범위 확장 설정 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// '불시엔' 체크박스 상태 변경 시 스킬 범위를 재계산하고 오버레이를 갱신합니다.
        /// </summary>
        private void checkBox_Skill_RangeExtensionItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.checkBox_Skill_RangeExtensionItem != null)
                {
                    this.Skill_RangeExtensionItem = this.checkBox_Skill_RangeExtensionItem.Checked;
                }
                this.SetSkillShape();
            }
            catch (Exception ex)
            {
                MessageBox.Show("불시엔 설정 실패: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// 투명도 트랙바 스크롤 시 호출되어 스킬 범위의 투명도 값을 실시간으로 조절합니다.
        /// </summary>
        private void Skill_RangeOpacity_Scroll(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.TrackBar trackBar = sender as System.Windows.Forms.TrackBar;
                if (trackBar != null)
                {
                    int opacityValue = trackBar.Value;

                    this.Skill_RangeOpacity = (byte)opacityValue;
                    this.SetSkillShape();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("투명도 조절 중 오류 발생: " + ex.Message);
            }
        }

        #endregion
    }
}