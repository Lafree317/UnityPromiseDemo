using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SLua;

public class LuaManager : MonoBehaviour {

    private LuaFunction _luaPromise = null;
    private LuaFunction _luaPromiseAll = null;
    private LuaFunction _luaPromiseCrash = null;
    private void Awake()
    {
        LuaSvr svr = new LuaSvr();// 如果不先进行某个LuaSvr的初始化的话,下面的mianState会爆一个为null的错误..
        LuaSvr.mainState.loaderDelegate += LuaReourcesFileLoader;
		svr.init(null, () => // 如果不用init方法初始化的话,在Lua中是不能import的
		{
            svr.start("Main");
            _luaPromise = LuaSvr.mainState.getFunction("LuaPromise");
            _luaPromiseAll = LuaSvr.mainState.getFunction("LuaPromiseAll");
            _luaPromiseCrash = LuaSvr.mainState.getFunction("LuaPromiseCrash");
		});
    }
    
    public void LuaPromise()
    {
        if(_luaPromise != null)
        {
            _luaPromise.call(); 
        }
    }
    public void LuaPromiseAll()
    {
        if(_luaPromiseAll != null)
        {
            _luaPromiseAll.call();
        }
    }

    public void LuaPromiseCrash()
    {
        if(_luaPromiseCrash != null)
        {
            _luaPromiseCrash.call();
        }
    }

    // SLua Loader代理方法
    private static byte[] LuaReourcesFileLoader(string strFile)
    {
        // 这里为了测试就不先判断为空,开发的时候再加上
        string filename = Application.dataPath + "/Scripts/Lua/" + strFile.Replace('.', '/') + ".txt";
        return File.ReadAllBytes(filename);
    }

}