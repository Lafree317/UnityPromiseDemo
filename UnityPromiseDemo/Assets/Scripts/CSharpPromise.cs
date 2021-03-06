﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;
using System;
using System.Diagnostics;

public class CSharpPromise : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
    public void PromiseFunc()
    {
        StepOne("第一步")
        .Then(value => StepTwo(value +"第二步 "))
        .Then(value => StepThree(value + "第三步"))
        .Done(value => print(value));
        //.Catch(error => print(error));
    }

    public void PromiseAllFunc()
    {
        IPromise<string>[] all = new IPromise<string>[]
        {
            StepOne("第一步"),
            StepTwo("第二步"),
            StepThree("第三步")
        };

        Promise<string>.All(all)
            .Then(value=> {
                foreach(string abc in value)
                {
                    print(abc);
                }
            })
            .Catch(error => print(error));
    }

    public void CrashProtectFunc()
    {
        StepOne("abc")
            .Then(value => CrashProtectFunc())
            .Catch(error => print(error));
    }

    IPromise<string> StepOne(string v)
    {
        var p = new Promise<string>();
        print("StepOne");
        var pro = Process.GetCurrentProcess();
        var pros = Process.GetProcesses();
        print(pro);
        print(pros);
        p.Resolve(v+" 完成 \n");
        return p;
	}

    IPromise<string> StepTwo(string v)
    {
        var p = new Promise<string>();
        print("StepTwo");
        p.Resolve(v + " 完成 \n");
        return p;
	}

    IPromise<string> StepThree(string v)
    {
        var p = new Promise<string>();
        print("StepThree");

        p.Resolve(v + " 完成 \n");
        return p;
	}

    IPromise<string> CrashStep()
    {
        var p = new Promise<string>();

        return p;
    }

    private void SystemException(string v)
    {
        throw new NotImplementedException();
    }
}
