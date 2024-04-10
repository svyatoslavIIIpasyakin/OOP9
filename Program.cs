using System;
using System.Collections.Generic;

// Базовый класс уведомления
public class Notification : IComparable<Notification>
{
    public string Message { get; set; }
    public DateTime TimeStamp { get; set; }

    public int CompareTo(Notification other)
    {
        return this.TimeStamp.CompareTo(other.TimeStamp);
    }
}

// Класс уведомления по SMS
public class SMSNotification : Notification
{
    public string PhoneNumber { get; set; }
}

// Класс уведомления по Email
public class EmailNotification : Notification
{
    public string EmailAddress { get; set; }
}

// Класс уведомления через Push-уведомления
public class PushNotification : Notification
{
    public string DeviceId { get; set; }
}

// Обобщенный контейнер уведомлений
public class NotificationContainer<T> where T : Notification
{
    private List<T> notifications = new List<T>();

    // Добавление уведомления в контейнер
    public void AddNotification(T notification)
    {
        notifications.Add(notification);
    }

    // Получение уведомления из контейнера
    public T GetNotification(int index)
    {
        if (index >= 0 && index < notifications.Count)
            return notifications[index];
        else
            throw new IndexOutOfRangeException("Index is out of range");
    }

    // Проверка наличия уведомлений в контейнере
    public bool HasNotifications()
    {
        return notifications.Count > 0;
    }

    // Сортировка уведомлений в контейнере
    public void SortNotifications()
    {
        notifications.Sort();
    }

    // Получение всех уведомлений из контейнера
    public List<T> GetNotifications()
    {
        return notifications;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем контейнеры с различными типами уведомлений
        NotificationContainer<SMSNotification> smsContainer = new NotificationContainer<SMSNotification>();
        NotificationContainer<EmailNotification> emailContainer = new NotificationContainer<EmailNotification>();
        NotificationContainer<PushNotification> pushContainer = new NotificationContainer<PushNotification>();

        // Добавляем уведомления в контейнеры
        smsContainer.AddNotification(new SMSNotification { Message = "SMS notification", PhoneNumber = "123456789" });
        emailContainer.AddNotification(new EmailNotification { Message = "Email notification", EmailAddress = "example@example.com" });
        pushContainer.AddNotification(new PushNotification { Message = "Push notification", DeviceId = "device123" });

        // Выводим содержимое контейнеров
        Console.WriteLine("SMS notifications:");
        foreach (var sms in smsContainer.GetNotifications())
        {
            Console.WriteLine($"Message: {sms.Message}, Phone number: {sms.PhoneNumber}");
        }

        Console.WriteLine("\nEmail notifications:");
        foreach (var email in emailContainer.GetNotifications())
        {
            Console.WriteLine($"Message: {email.Message}, Email address: {email.EmailAddress}");
        }

        Console.WriteLine("\nPush notifications:");
        foreach (var push in pushContainer.GetNotifications())
        {
            Console.WriteLine($"Message: {push.Message}, Device ID: {push.DeviceId}");
        }

        // Проверяем наличие уведомлений в контейнерах
        Console.WriteLine("\nDoes SMS container have notifications? " + smsContainer.HasNotifications());
        Console.WriteLine("Does Email container have notifications? " + emailContainer.HasNotifications());
        Console.WriteLine("Does Push container have notifications? " + pushContainer.HasNotifications());

        // Сортируем уведомления в контейнерах
        smsContainer.SortNotifications();
        emailContainer.SortNotifications();
        pushContainer.SortNotifications();
    }
}
