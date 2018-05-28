using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSG;
using System;

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

    IPromise<string> StepOne(string v)
    {
        var p = new Promise<string>();
        print("StepOne");
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

        //Exception e = new Exception("错误");
        //p.Reject(e);
        p.Resolve(v + " 完成 \n");
        return p;
	}

    private void SystemException(string v)
    {
        throw new NotImplementedException();
    }
}
