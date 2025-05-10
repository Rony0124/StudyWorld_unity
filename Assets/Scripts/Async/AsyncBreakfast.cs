using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

internal class Bacon { }
internal class Coffee { }
internal class Egg { }
internal class Juice { }
internal class Toast { }

public class AsyncBreakfast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    private async Task Init() {
        Coffee cup = PourCoffee();
        Debug.Log("Coffee is ready");

        var eggsTask = FryEggsAsync(2);
        var baconTask = FryBaconAsync(3);
        var toastTask = MakeToastWithButterAndJamAsync(2);

        var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
        while (breakfastTasks.Count > 0) {
            Task finishedTask = await Task.WhenAny(breakfastTasks);
            if (finishedTask == eggsTask) {
                Debug.Log("eggs are ready");
            }
            else if (finishedTask == baconTask) {
                Debug.Log("bacon is ready");
            }
            else if (finishedTask == toastTask) {
                Debug.Log("toast is ready");
            }
            breakfastTasks.Remove(finishedTask);
        }

        Juice oj = PourOJ();
        Debug.Log("oj is ready");
        Debug.Log("Breakfast is ready!");

    }

    private static Coffee PourCoffee() {
        Debug.Log("pouring coffee");
        return new Coffee();
    }

    private static async Task<Egg> FryEggsAsync(int howMany) {
        Debug.Log("warming the egg pan");
        await Task.Delay(3000);
        Debug.Log("cooking the eggs");
        await Task.Delay(3000);
        Debug.Log("put eggs on plate");

        return new Egg();
    }

    private static async Task<Bacon> FryBaconAsync(int slices) {
        Debug.Log($"putting {slices} slices of bacon in the pan");
        Debug.Log("cooking first side of bacon...");
        await Task.Delay(4000);
        for (int slice = 0; slice < slices; slice++) {
            Debug.Log("flipping a slice of bacon");
        }
        Debug.Log("cooking the second side of bacon...");
        await Task.Delay(3000);
        Debug.Log("Put bacon on plate");

        return new Bacon();
    }

    private static async Task<Toast> ToastBreadAsync(int slices) {
        for (int i = 0; i < slices; i++) {
            Debug.Log("putting a sliceof bread in the toaster");
        }
        Debug.Log("start toasting..");
        await Task.Delay(5000);
        Debug.Log("Remove toast from toaster");

        return new Toast();
    }
    private static void ApplyJam(Toast toast) =>
        Debug.Log("Putting jam on the toast");

    private static void ApplyButter(Toast toast) =>
        Debug.Log("Putting butter on the toast");

    static async Task<Toast> MakeToastWithButterAndJamAsync(int number) {
        var toast = await ToastBreadAsync(number);
        ApplyButter(toast);
        ApplyJam(toast);

        return toast;
    }

    private static Juice PourOJ() {
        Debug.Log("Pouring orange juice");
        return new Juice();
    }
}
