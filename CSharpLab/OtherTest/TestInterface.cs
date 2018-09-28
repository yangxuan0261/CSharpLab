using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

/// <summary>
/// 逆变 与 协变
/// http://www.cnblogs.com/LoveJenny/archive/2012/03/13/2392747.html
/// </summary>

class TestInterface {

    class Animal {
    }

    class Dog : Animal {
    }

    public interface IMyList1<out T> {
        T GetElement();
    }

    public class MyList1<T> : IMyList1<T> {
        public T GetElement() {
            return default(T);
        }

    }

    public static void test1() {
        IMyList1<Dog> myDogs = new MyList1<Dog>();
        IMyList1<Animal> myAnimals = myDogs;
    }

    //-------------------------------
    public interface IMyList2<out T> {
        T GetElement();
        //void ChangeT(T t); // 这里编译错误，因为 T 被 out 修饰，所以 T 只能做返回值，不能做参数
    }

    public class MyList2<T> : IMyList2<T> {
        public T GetElement() {
            return default(T);
        }

        public void ChangeT(T t) {
            //Change T
        }
    }

    //-------------------------------
    public interface IMyList<in T> {
        //T GetElement(); // 这里编译错误，因为 T 被 in 修饰，所以 T 只能做参数，不能做返回值
        void ChangeT(T t);

    }

    //没有指定 in 或者 out，就可以即做 参数 又做 返回值
    public class MyList<T> : IMyList<T> {
        public T GetElement() {
            return default(T);
        }

        public void ChangeT(T t) {
            //Change T
        }
    }
}
