Some some = new Some();
some.SomeMethod();

class Some
{
    public void SomeMethod()
    {
        lock (this)
        {
            // Этот код имеет экслюзивный доступ к данным...
        }
    }

    public void SomeMethodLock()
    {
        Boolean lockTaken = false;
        try
        {
            //
            Monitor.Enter(this, ref lockTaken);
            // Этот код имеет экслюзивный доступ к данным...
        }
        finally
        {
            if (lockTaken) Monitor.Exit(this);
        }
    }
}