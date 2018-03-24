using System;
using ExpectedObjects;
using LinqTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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
}