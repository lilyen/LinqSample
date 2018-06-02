using System;
using System.Collections;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.FindProductByPrice(product => product.FindTopSaleProduct());

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void find_employee_that_age_older_than_30()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.FindProductByPrice(p => p.Age > 30);

            var expected = new List<Employee>()
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void find_employee_that_age_older_than_30_and()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.Find((p, i) => p.Age > 30 && i >= 2);

            var expected = new List<Employee>()
            {
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6}
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void find_products_that_price_between_200_and_500_Linq()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.Where(p => p.Price > 200 && p.Price < 500 && p.Cost > 30);

            var expected = new List<Product>()
            {
                new Product{Id=3, Cost=31, Price=310, Supplier="Odd-e" },
                new Product{Id=4, Cost=41, Price=410, Supplier="Odd-e" }
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void changeHttpToHttps()
        {
            var urls = RepositoryFactory.GetUrls();
            var httpsUrls = WithoutLinq.ToHttps(urls);

            var expected = new List<string>()
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com"
            };

            expected.ToExpectedObject().ShouldEqual(httpsUrls.ToList());
        }

        [TestMethod]
        public void GetUrlLength()
        {
            var urls = RepositoryFactory.GetUrls();
            IEnumerable<int> httpsUrls = WithoutLinq.GetUrlsLength(urls, url => url.Length);

            var expected = new List<int>()
            {
                19,
                20,
                19,
                17
            };

            expected.ToExpectedObject().ShouldEqual(httpsUrls.ToList());
        }

        [TestMethod]
        public void TestSelect()
        {
            var enumerable = RepositoryFactory.GetEmployees();
            var acturl = enumerable.LilyWhere(e => e.Age < 25).LilySelect(w => $"{w.Role}:{w.Name}");

            foreach (var titleNeam in acturl)
            {
                Console.WriteLine(titleNeam);
            }

            var expected = new List<string>()
            {
                "OP:Andy",
                "Engineer:Frank"
            };

            expected.ToExpectedObject().ShouldEqual(acturl.ToList());
        }

        [TestMethod]
        public void f()
        {
            var enumerable = RepositoryFactory.GetEmployees();
            IEnumerable<Employee> actual = enumerable.LilyTake(2);

            var expected = new List<Employee>()
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Testskip()
        {
            var enumerable = RepositoryFactory.GetEmployees();
            IEnumerable<Employee> actual = enumerable.LilySkip(6);

            var expected = new List<Employee>()
            {
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void TestSum()
        {
            var enumerable = RepositoryFactory.GetEmployees();
            var actual = enumerable.LilyGetPageSum(3, e => e.MonthSalary);

            var expected = new List<int>()
            {
                620,540,370
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void TestTakeWhile()
        {
            var enumerable = RepositoryFactory.GetEmployees();
            var actual = enumerable.LilyTakeWhile(2, e => e.MonthSalary > 150);

            var expected = new List<Employee>()
            {
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void TestSkipWhile()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.LilySkipWhile(3, e => e.MonthSalary < 150);

            var expected = new List<Employee>()
            {
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void TestAny()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.IsFalse(employees.LilyAny(e => e.MonthSalary > 500));
        }

        [TestMethod]
        public void TestAnyNull()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.IsTrue(employees.LilyAny());
        }

        [TestMethod]
        public void TestAll()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.IsFalse(employees.LilyAll(x=>x.MonthSalary>200));
        }

        [TestMethod]
        public void TestFirstOrDefault()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.AreEqual("Kevin",employees.LilyFirstOrDefault(x=>x.MonthSalary>200).Name);
            Assert.IsNull(employees.LilyFirstOrDefault(x=>x.MonthSalary>500));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestFitst()
        {
            var employees = RepositoryFactory.GetEmployees();
            employees.LilyFirst(e => e.MonthSalary > 500);
        }

        [TestMethod]
        public void TestSinge()
        {
            var employees = RepositoryFactory.GetEmployees();
            Assert.AreEqual(RoleType.Manager, employees.LilySingle(e => e.Role == RoleType.Manager).Role);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSingle2()
        {
            var employees = RepositoryFactory.GetEmployees();
            employees.LilySingle(e => e.Role == RoleType.Unknown);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSingle3()
        {
            var employees = RepositoryFactory.GetEmployees();
            employees.LilySingle(e => e.Role == RoleType.Engineer);
        }

        [TestMethod]
        public void TestDistinct()
        {
            var expected = new List<Employee>
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Andy", Role=RoleType.OP, MonthSalary=80, Age=22, WorkingYear=2.6} ,             
            }.ToExpectedObject();


            expected.ShouldEqual(RepositoryFactory.GetEmployees().LilyDistinct(new MyCompareRole()).ToList());
        }
        [TestMethod]
        public void MyTestMethod()
        {
           var expected = new Employee()
            {
                Name = "lulu"
            };
            var employees = RepositoryFactory.GetEmployees();
            var younger = employees.Where(x=>x.Age<=15);
            var actual = DunkDefaultIfEmpty(younger);
            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
        [TestMethod]
        public void TestContain()
        {
            var products = RepositoryFactory.GetProducts();
            var luckyProduct = new Product{ Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e" };
            Assert.IsTrue(products.LilyContain(luckyProduct, new luckyCompary()));
        }

        [TestMethod]
        public void TestSequence()
        {
            var products = RepositoryFactory.GetProducts();
            var anotherProducts = RepositoryFactory.GetAnotherProducts();
            Assert.IsFalse(LilySequence(products, anotherProducts, new luckyCompary()));
        }

        [TestMethod]
        public void TestSequence2()
        {
            var products = RepositoryFactory.GetProducts();
            var anotherProducts = RepositoryFactory.GetProductsS();
            Assert.IsFalse(LilySequence(products, anotherProducts, new luckyCompary()));
        }

        [TestMethod]
        public void TestSequence3()
        {
            var products = RepositoryFactory.GetProducts();
            var anotherProducts = RepositoryFactory.GetProducts();
            Assert.IsTrue(LilySequence(products, anotherProducts, new luckyCompary()));
        }

        [TestMethod]
        public void TestSequence4()
        {
            var products = RepositoryFactory.GetProductsS();
            var anotherProducts = RepositoryFactory.GetProducts();
            Assert.IsFalse(LilySequence(products, anotherProducts, new luckyCompary()));
        }

        private bool LilySequence<TSource>(IEnumerable<TSource> sources, IEnumerable<TSource> anotherProducts, IEqualityComparer<TSource> Compary)
        {
            var source = sources.GetEnumerator();
            var anotherSource = anotherProducts.GetEnumerator();

            while (true)
            {
                var a = source.MoveNext();
                var b = anotherSource.MoveNext();

                if (a && b)
                {
                    if (!Compary.Equals(source.Current, anotherSource.Current))
                    {
                        return false;
                    }
                }
                else
                {
                    break;
                }
            }

            return source.Current == null && anotherSource.Current == null;
        }

        private static IEnumerable<Employee> DunkDefaultIfEmpty(IEnumerable<Employee> younger)
        {
            foreach (var employee in younger)
            {
                if (employee!=null)
                {
                    yield return  employee;
                }
            }

            yield return new Employee() {Name = "lulu"};
        }
    }
}

internal static class WithoutLinq
{
    public static IEnumerable<T> FindProductByPrice<T>(this IEnumerable<T> products, Func<T, bool> predicate)
    {
        foreach (var product in products)
        {
            if (predicate(product))
            {
                yield return product;
            }
        }
    }

    public static IEnumerable<T> Find<T>(this IEnumerable<T> products, Func<T, int, bool> predicate)
    {
        var index = 0;
        foreach (var product in products)
        {
            if (predicate(product, index))
            {
                yield return product;
            }

            index++;
        }
    }

    public static IEnumerable<string> ToHttps(IEnumerable<string> urls)
    {
        foreach (var url in urls)
        {
            yield return url.Replace("http://", "https://");
        }
    }

    public static IEnumerable<TResult> GetUrlsLength<TSource, TResult>(IEnumerable<TSource> urls, Func<TSource, TResult> predicate)
    {
        foreach (var url in urls)
        {
            yield return predicate(url);
        }
    }


}

internal static class YourOwnLinq
{
    public static IEnumerable<TSource> LilyDistinct<TSource>(this IEnumerable<TSource> source,
        IEqualityComparer<TSource> MyCompare)
    {
        var compare = MyCompare ?? EqualityComparer<TSource>.Default;
        var hashSet = new HashSet<TSource>(compare);
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (hashSet.Add(enumerator.Current))
            {
                yield return enumerator.Current;
            }
        }
    }
    public static TSource LilySingle<TSource>(this IEnumerable<TSource> employees, Func<TSource, bool> predicate)
    {
        var enumerator = employees.GetEnumerator();
        var index = 0;
        TSource e = default(TSource);
        while (enumerator.MoveNext() )
        {
            if (predicate(enumerator.Current))
            {
                index += 1;
                e = enumerator.Current;
            }

            if (index > 1)
            {
                throw new InvalidOperationException();
            }
        }

        if (index==0)
        {
            throw new InvalidOperationException();
        }

        return e;
    }

    public static IEnumerable<TSource> LilyWhere<TSource>(this IEnumerable<TSource> items, Func<TSource, bool> predicate)
    {
        foreach (var item in items)
        {
            if (predicate(item))
            {
                yield return item;
            }
        }
    }

    public static TSource LilyFirst<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, bool> func)
    {
        var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (func(enumerator.Current))
            {
                return enumerator.Current;
            }
        }

        throw new ArgumentNullException();
    }

    public static TSource LilyFirstOrDefault<TSource>(this IEnumerable<TSource> employees, Func<TSource, bool> predicate)
    {
        var enumerator = employees.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                return enumerator.Current;
            }
        }

        return default(TSource);
    }

    public static bool LilyAll<TSource>(this IEnumerable<TSource> employees, Func<TSource, bool> func)
    {
        var enumerator = employees.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (!func(enumerator.Current))
            {
                return false;
            }
        }

        return true;
    }

    public static bool LilyAny<TSource>(this IEnumerable<TSource> items, Func<TSource, bool> predicate)
    {
        foreach (var item in items)
        {
            if (predicate(item))
            {
                return true;
            }
        }

        return false;
    }

    public static IEnumerable<TSource> LilyWhere<TSource>(this IEnumerable<TSource> items, Func<TSource, int, bool> predicate)
    {
        var index = 0;
        foreach (var item in items)
        {
            if (predicate(item, index))
            {
                yield return item;
            }

            index++;
        }
    }

    public static IEnumerable<TResult> LilySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> predicate)
    {
        foreach (var url in urls)
        {
            yield return predicate(url);
        }
    }

    public static IEnumerable<TSource> LilyTake<TSource>(this IEnumerable<TSource> items, int number)
    {

        //return items.LilyWhere((item, i) => i < index); //where run all 8

        var index = 0;
        var enumerator = items.GetEnumerator();
        while (enumerator.MoveNext() && index < number)
        {
            yield return enumerator.Current;
            index++;
        }
    }

    public static IEnumerable<TSource> LilySkip<TSource>(this IEnumerable<TSource> items, int number)
    {
        var index = 0;
        var enumerator = items.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (index >= number)
            {
                yield return enumerator.Current;
            }
            index++;
        }
    }

    public static IEnumerable<int> LilyGetPageSum<TSource>(this IEnumerable<TSource> items, int pageSize, Func<TSource, int> summarize)
    {
        var index = 0;

        //while (index < items.Count())
        //{
        //    yield return items.Skip(index).Take(pageSize).Sum(summarize);
        //    index += pageSize;
        //}

        //var result = items.LilySkip(index).LilyTake(pageSize).Sum(summarize);
        //while (result != 0)
        //{
        //    yield return result;
        //    index += pageSize;
        //    result = items.LilySkip(index).LilyTake(pageSize).Sum(summarize);
        //}

        int result;
        do
        {
            result = items.LilySkip(index).LilyTake(pageSize).Sum(summarize);
            if (result == 0)
            {
                yield break;
            }

            yield return result;
            index += pageSize;
        } while (true);
    }

    public static IEnumerable<TSource> LilyTakeWhile<TSource>(this IEnumerable<TSource> items, int count, Func<TSource, bool> func)
    {
        var index = 0;
        var enumerator = items.GetEnumerator();

        while (enumerator.MoveNext() && index < count)
        {
            if (func(enumerator.Current))
            {
                yield return enumerator.Current;
                index++;
            }
        }
    }

    public static IEnumerable<TSource> LilySkipWhile<TSource>(this IEnumerable<TSource> items, int count, Func<TSource, bool> predicate)
    {
        var index = 0;
        var enumerator = items.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (index < count && predicate(enumerator.Current))
            {
                index++;
                continue;
            }

            yield return enumerator.Current;
        }
    }

    public static bool LilyAny<TSource>(this IEnumerable<TSource> items)
    {
        var enumerator = items.GetEnumerator();
        return enumerator.MoveNext();
    }

    public static bool LilyContain<TSource>(this IEnumerable<TSource> sources, TSource luckyProduct, IEqualityComparer<TSource> luckyCompary)
    {
        var enumerator = sources.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (luckyCompary.Equals(enumerator.Current, luckyProduct))
            {
                return true;
            }
        }

        return false;
    }
}

internal class MyCompareRole : IEqualityComparer<Employee>
{
     public bool Equals(Employee x, Employee y)
     {
         return x.Role == y.Role;
     }

    public int GetHashCode(Employee obj)
    {
        return obj.Role.GetHashCode();
    }
}

internal class luckyCompary : IEqualityComparer<Product>
{
    public bool Equals(Product x, Product y)
    {
        return x.Cost == y.Cost && x.Id==y.Id && x.Price==y.Price && x.Supplier == y.Supplier;
    }

    public int GetHashCode(Product obj)
    {
        return 0;
    }
}

