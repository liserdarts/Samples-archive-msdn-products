Imports Microsoft.VisualBasic
	Imports System
	Imports System.Collections.Generic
	Imports System.Linq
	Imports Microsoft.ApplicationServer.Caching
	Imports System.Text
Namespace CacheAPISample


	' A Sample class to represent a composite object to be put in cache.
	<Serializable> _
	Friend Class ProductInfo
		Private privateId As Integer
		Public Property Id() As Integer
			Get
				Return privateId
			End Get
			Set(ByVal value As Integer)
				privateId = value
			End Set
		End Property
		Private privateProductName As String
		Public Property ProductName() As String
			Get
				Return privateProductName
			End Get
			Set(ByVal value As String)
				privateProductName = value
			End Set
		End Property
		Private privateValue As Double
		Public Property Value() As Double
			Get
				Return privateValue
			End Get
			Set(ByVal value As Double)
				privateValue = value
			End Set
		End Property
		Private privateImage As Byte()
		Public Property Image() As Byte()
			Get
				Return privateImage
			End Get
			Set(ByVal value As Byte())
				privateImage = value
			End Set
		End Property


		Public Overrides Function ToString() As String
			Dim strb As New StringBuilder()
			strb.AppendFormat(" Id: {0}, ProductName:{1}, Value:{2}", Id, ProductName, Value)
			If Nothing IsNot Image AndAlso 0 <> Image.Length Then
				strb.Append(", Image:")
				Dim sep As String = ""
				For i As Integer = 0 To Image.Length - 1
					strb.AppendFormat("{0}{1}",sep, Image(i))
					sep = ";"
				Next i
			End If

			Return strb.ToString()
		End Function
	End Class

	Friend Class Program
		Private myDefaultCache As DataCache
		Private myObjectForCaching As String = "This is my Object"

		Shared Sub Main(ByVal args() As String)
			Dim program As New Program()

			program.PrepareClient()
			program.RunSampleTest()

			Console.WriteLine("Press any key to continue ...")
			Console.ReadLine()
		End Sub

		Public Sub RunSampleTest()
			' TESTING SIMPLE Add/Get on default cache
			AddGetDefaultCache()

			' TESTING SIMPLE Add/GetAndLock
			AddGetAndLock()

			' TESTING SIMPLE Add/Get with Version
			AddGetWithVersion()
		End Sub

		Private Sub AddGetWithVersion()
			'
			' TESTING SIMPLE Add/Get with Version
			'

			Dim itemVersion As DataCacheItemVersion
			Dim cacheItem1, cacheItem2 As DataCacheItem
			Dim cacheItemVersion As DataCacheItemVersion
			Console.ForegroundColor = ConsoleColor.Gray
			Console.WriteLine("-----------------------------------")
			Console.WriteLine("Testing Simple Add/GetCacheItem/Put")
			Console.WriteLine("Cache       = default")
			Console.WriteLine("Tags        = Not Supported")
			Console.WriteLine("Version     = yes")

			Dim KeyToMyStringWithVersion As String = "KeyToMyStringWithVersion"


			Try
				'First, clear any old value in cache due to some previous run of the sample
				TryClearOldKey(myDefaultCache, KeyToMyStringWithVersion)

				' Add an object to the cache
				itemVersion = myDefaultCache.Add(KeyToMyStringWithVersion, myObjectForCaching)
				If itemVersion IsNot Nothing Then
					WriteSuccess("Add-Object Added to Cache. [key={0}]", KeyToMyStringWithVersion)
				Else
					WriteFailure("Add-Object did not add to cache [key={0}]", KeyToMyStringWithVersion)
				End If

				' Get the object added to the Cache
				cacheItem1 = myDefaultCache.GetCacheItem(KeyToMyStringWithVersion)
				If cacheItem1 IsNot Nothing Then
					WriteSuccess("GetCacheItem-Object Get from cache [key={0}]", KeyToMyStringWithVersion)
				Else
					WriteFailure("GetCacheItem-Object did not Get from cache [key={0}]", KeyToMyStringWithVersion)
				End If

				' Get another copy of the same object (used to remember the version)
				cacheItem2 = myDefaultCache.GetCacheItem(KeyToMyStringWithVersion)
				If cacheItem2 IsNot Nothing Then
					WriteSuccess("GetCacheItem-Object Get from cache [key={0}]", KeyToMyStringWithVersion)
				Else
					WriteFailure("GetCacheItem-Object did not Get from cache [key={0}]", KeyToMyStringWithVersion)
				End If

				' Add a newer version of the object to the cache, supply the version as well to ensure that we are updating
				' the cache only if we have the latest version
				cacheItemVersion = myDefaultCache.Put(KeyToMyStringWithVersion, CObj(cacheItem1.Value), cacheItem1.Version)
				If cacheItemVersion IsNot Nothing Then
					WriteSuccess("Put-Object updated successfully [key={0}]", KeyToMyStringWithVersion)
					WriteSuccess("          New version {0} Old version",If(cacheItemVersion > cacheItem2.Version, ">", "<="))
				Else
					WriteFailure("Put-Object did not update successfully [key={0}]", KeyToMyStringWithVersion)
				End If

				' Try to add an object when the version of the object in the Cache is newer, it will fail
				Try
					cacheItemVersion = myDefaultCache.Put(KeyToMyStringWithVersion, CObj(cacheItem2.Value), cacheItem2.Version)
					If cacheItemVersion IsNot Nothing Then
						WriteFailure("Put-Object update. Update to new version work.  [key={0}]", KeyToMyStringWithVersion)
					Else ' this will throw a exception, so the else will not run if the object is locked.
						WriteFailure("[Code path should not be hit if object is locked]Put-Object did not update. Update to new version worked.  [key={0}]", KeyToMyStringWithVersion)
					End If
				Catch ex As DataCacheException
					WriteSuccess("Put-Object-Expected behaviour since Object is newer. Exception: {0}", ex.Message)
				End Try
			Catch ex As DataCacheException
				WriteSuccess("Distributed Cache Generated Exception: {0}", ex.Message)
			End Try
		End Sub

		Private Sub AddGetAndLock()
			'
			' TESTING SIMPLE Add/GetAndLock
			' without version
			'
			' Try this variation
			' - Put a BreakPoint on the second GetAndLock, and hold the execution for 5 seconds.
			'   It will return the object and lock the object for 10 seconds, since the first lock has expired.

			Console.ForegroundColor = ConsoleColor.Gray
			Dim itemVersion As DataCacheItemVersion
			Dim item As String
			Console.WriteLine("-----------------------------")
			Console.WriteLine("Testing Simple Add/Get/GetAndLock/GetIfNewer/Put/PutAndUnlock")
			Console.WriteLine("Cache       = default")
			Console.WriteLine("Tags        = Not Supported")
			Console.WriteLine("Version     = <none>")

			Dim myVersionBeforeChange As DataCacheItemVersion = Nothing, myVersionAfterChange As DataCacheItemVersion = Nothing, myVersionChangedOnceMore As DataCacheItemVersion = Nothing
            Dim lockHandle As DataCacheLockHandle = Nothing
			Dim myKey As String = "KeyToMyStringTryingLock"

			Try
				'First, clear any old value in cache due to some previous run of the sample
				TryClearOldKey(myDefaultCache, myKey)

				' Initialize the object with a Add
				itemVersion = myDefaultCache.Add(myKey, myObjectForCaching)
				If itemVersion IsNot Nothing Then
				   WriteSuccess("Add-Object Added to Cache [key={0}]", myKey)
				Else
				   WriteFailure("Add-Object did not add to cache [key={0}]", myKey)
				End If

				' Do a Simple Get, lock the object for 5 seconds
                item = CStr(myDefaultCache.GetAndLock(myKey, New TimeSpan(0, 0, 5), lockHandle))
                If item IsNot Nothing Then
                    WriteSuccess("GetAndLock-Object Get from cache [key={0}]", myKey)
                Else
                    WriteFailure("GetAndLock-Object did not Get from cache [key={0}]", myKey)
                End If

                ' Do a optimistic Get
                item = CStr(myDefaultCache.Get(myKey, myVersionBeforeChange))
                If item IsNot Nothing Then
                    WriteSuccess("Get-Object returned. Get will always pass. Will not wait")
                    WriteSuccess("          on a updating object. Current Version will be returned. [key={0}]", myKey)
                Else
                    WriteFailure("Get-Object did not return. [key={0}]", myKey)
                End If

                Try
                    ' Do a one more Simple Get, and attempt lock the object for 10 seconds
                    item = CStr(myDefaultCache.GetAndLock(myKey, New TimeSpan(0, 0, 10), lockHandle))
                    If item IsNot Nothing Then
                        WriteFailure("GetAndLock-Object Get from cache [key={0}]", myKey)
                    Else
                        ' Since a exception will catch it, this will never return null
                        WriteFailure("[This code path should not be hit] GetAndLock-Object did not Get from cache [key={0}]", myKey)
                    End If
                Catch ex As DataCacheException
                    WriteSuccess("GetAndLock hit a exception, because Object is already locked. [key={0}]", myKey)
                    WriteSuccess("Expected GetAndLock-Distributed Cache Generated Exception: {0}", ex.Message)
                End Try

                ' Get the Object only if the version has changed
                item = CStr(myDefaultCache.GetIfNewer(myKey, myVersionBeforeChange))
                If item IsNot Nothing Then
                    WriteFailure("GetIfNewer-Object changed. Should not return as Object has")
                    WriteFailure("            not been changed. [key={0}]", myKey)
                Else
                    WriteSuccess("GetIfNewer-Object has not changed. Hence did not return. [key={0}]", myKey)
                End If

                ' Now update the object with a Put                
                myVersionAfterChange = CType(myDefaultCache.Put(myKey, myObjectForCaching & "Put1"), DataCacheItemVersion)
                If myVersionAfterChange IsNot Nothing Then
                    WriteSuccess("Put1-null-version-Object changed. Put will pass even if Object")
                    WriteSuccess("          is locked. Object will also be unlocked. [key={0}]", myKey)
                    myObjectForCaching &= "Put1"
                Else
                    WriteFailure("Put1-null-version-Object did not change. [key={0}]", myKey)
                End If

                ' Object with older version changed
                item = CStr(myDefaultCache.GetIfNewer(myKey, myVersionBeforeChange))
                If item IsNot Nothing Then
                    WriteSuccess("GetIfNewer-Object has been changed. [key={0}]", myKey)
                Else
                    WriteFailure("GetIfNewer-Object did not return. Put ")
                    WriteFailure("            did modify the Object. Should return. [key={0}]", myKey)
                End If

                ' Object with newer version after Put
                item = CStr(myDefaultCache.GetIfNewer(myKey, myVersionAfterChange))
                If item IsNot Nothing Then
                    WriteFailure("GetIfNewer-Object with newer version not changed.")
                    WriteFailure("            Should not return. [key={0}]", myKey)
                Else
                    WriteSuccess("GetIfNewer-Object with newer version not changed. [key={0}]", myKey)
                End If

                ' Object with newer version after Put
                myVersionChangedOnceMore = CType(myDefaultCache.Put(myKey, myObjectForCaching & "Put2", myVersionBeforeChange), DataCacheItemVersion)
                If myVersionChangedOnceMore IsNot Nothing Then
                    WriteSuccess("Put2-version from Put1-Object changed. [key={0}]", myKey)
                    myObjectForCaching &= "Put2"
                Else
                    WriteFailure("Put2-version from Put1-Object did not change. [key={0}]", myKey)
                End If

                Try
                    ' Try the above PutAndUnlock                 
                    myVersionChangedOnceMore = CType(myDefaultCache.PutAndUnlock(myKey, myObjectForCaching & "Put3", lockHandle), DataCacheItemVersion)
                    If myVersionChangedOnceMore IsNot Nothing Then
                        WriteFailure("[This code should not be hit]PutAndUnlock-Object updated and unlocked. [key={0}]", myKey)
                        myObjectForCaching &= "Put3"
                    Else
                        WriteFailure("PutAndUnlock-Object should have updated and unlocked. [key={0}]", myKey)
                    End If
                Catch ex As DataCacheException
                    WriteSuccess("PutAndUnlock-Expected exception since Object is already unlocked. [key={0}]", myKey)
                    WriteSuccess("PutAndUnlock-Distributed Cache Generated Exception: {0}", ex.Message)
                End Try

                ' Unlock Object
                Try
                    myDefaultCache.Unlock(myKey, lockHandle)
                    WriteFailure("[This code should not be hit]Unlock-Object unlocked. [key={0}]", myKey)
                Catch ex As DataCacheException
                    WriteSuccess("Unlock-Expected exception since Object is already unlocked. [key={0}]", myKey)
                    WriteSuccess("Expected Unlock-Distributed Cache Generated Exception: {0}", ex.Message)
                End Try

                ' Finally, Test the state of object should be "This is my Object.Put1Put2"
                item = CStr(myDefaultCache.Get(myKey, myVersionChangedOnceMore))
                If item = myObjectForCaching Then
                    WriteSuccess("Get-Object retrived from cache. [key={0}]", myKey)
                Else
                    WriteFailure("Get-Object was not retrived from cache. [key={0}]", myKey)
                End If
            Catch ex As DataCacheException
                WriteFailure("Add-Get-GetAndLock-GetIfVersionMismatch-Put-PutAndUnlock-Distributed Cache Generated Exception:")
                WriteFailure(ex.ToString())
            End Try
        End Sub

        Private Sub AddGetDefaultCache()
            Dim itemVersion As DataCacheItemVersion
            Dim item As String
            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine()
            '
            ' TESTING SIMPLE Add/Get on default cache
            '
            ' no regions
            '           
            ' Try this variation
            ' - Put a BreakPoint at the Get("KeyToMyString") call, and wait for 10 mins when breakpoint is hit.
            ' - Get will fail, as the data will have been expired (default TTL is 10 mins).
            Console.WriteLine("----------------------")
            Console.WriteLine("Testing Simple Add/Get")
            Console.WriteLine("Cache       = default")
            Console.WriteLine("Region      = Not Supported")
            Console.WriteLine("Tags        = Not Supported")
            Console.WriteLine("Version     = <none>")

            Try
                'First, clear any old value in cache due to some previous run of the sample
                TryClearOldKey(myDefaultCache, "KeyToMyString")

                ' Add-Get String:
                ' Store the object in the default Cache with a Add
                itemVersion = myDefaultCache.Add("KeyToMyString", myObjectForCaching)
                If itemVersion IsNot Nothing Then
                    WriteSuccess("Add-Object Added to Cache [key=KeyToMyString]")
                Else
                    WriteFailure("**Add-Object did not add to cache")
                End If

                ' Do a Simple Get using valid Key from the default Cache
                item = CStr(myDefaultCache.Get("KeyToMyString"))
                If item IsNot Nothing Then
                    WriteSuccess("Get-Object Get from cache [key=KeyToMyString]")
                Else
                    WriteFailure("Get-Object did not Get from cache [key=KeyToMyString]")
                End If

                ' Do a Simple Get using an invalid Key from the default Cache
                item = CStr(myDefaultCache.Get("InCorrectKeySpecified"))
                If item Is Nothing Then
                    WriteSuccess("Get-Object did not Get, since invalid key specified [key=InCorrectKeySpecified]")
                Else
                    WriteFailure("Get-Object Get from cache, unexpected result")
                End If

                'Remove the key that was added earlier 
                myDefaultCache.Remove("KeyToMyString")

                ' Add-Get Composite object:
                Dim productItem As ProductInfo = New ProductInfo With {.Id = 101, .ProductName = "Contoso Alpha", .Value = 202.1, .Image = New Byte() {1, 127, 255, 1, 127, 255}}

                'First, clear any old value in cache due to some previous run of the sample
                TryClearOldKey(myDefaultCache, productItem.ProductName)

                Dim productItemFromCache As ProductInfo = Nothing
                ' Store the object in the default Cache with a Add
                itemVersion = myDefaultCache.Add(productItem.ProductName, productItem)
                If itemVersion IsNot Nothing Then
                    WriteSuccess("Add-Object Added to Cache [key={0}]", productItem.ProductName)
                Else
                    WriteFailure("**Add-Object did not add to cache")
                End If

                ' Do a Simple Get using valid Key from the default Cache
                productItemFromCache = CType(myDefaultCache.Get(productItem.ProductName), ProductInfo)
                If productItemFromCache IsNot Nothing Then
                    WriteSuccess("Get-Object Get from cache [key={0}]", productItem.ProductName)
                    WriteSuccess("Got object: {0}", productItemFromCache.ToString())

                Else
                    WriteFailure("Get-Object did not Get from cache [key={0}]", productItem.ProductName)
                End If


                ' Do a Simple Get using an invalid Key from the default Cache
                productItemFromCache = CType(myDefaultCache.Get("InCorrectKeySpecified"), ProductInfo)
                If productItemFromCache Is Nothing Then
                    WriteSuccess("Get-Object did not Get, since invalid key specified [key=InCorrectKeySpecified]")
                Else
                    WriteFailure("Get-Object Get from cache, unexpected result")
                End If

                'Remove the key that was added earlier 
                myDefaultCache.Remove(productItem.ProductName)

            Catch ex As DataCacheException
                WriteFailure("Add-Get-This is failing probably because you are running this")
                WriteFailure("          sample test within 10mins (default TTL for the named cache). Please wait for sometime and try again")
                WriteFailure("Expected Distributed Cache Generated Exception: {0}", ex.Message)
            End Try
        End Sub

        Private Sub PrepareClient()
            Dim hostName As String = "[Cache endpoint without port]" 'Example : "CacheINT3General.cache.windows.net";
            Dim cachePort As Integer = 22233
            Dim server As New List(Of DataCacheServerEndpoint)()
            server.Add(New DataCacheServerEndpoint(hostName, cachePort))
            Dim config As New DataCacheFactoryConfiguration()

            'Insert the Authentication Token as shown below
            'Example : string authenticationToken = @"YWNzOkNhY2hlSU5UM0QzRGVtby5jYWNoZS5pbnQzLndpbmRvd3MtaW50Lm5ldC8=";
            Dim authenticationToken As String = "[InsertAuthenticationTokenHere]"
            config.SecurityProperties = New DataCacheSecurity(authenticationToken)

            config.Servers = server
           
            config.RequestTimeout = New TimeSpan(0, 0, 45)
            config.ChannelOpenTimeout = New TimeSpan(0, 0, 45)
            config.MaxConnectionsToServer = 5
            config.TransportProperties = New DataCacheTransportProperties() With {.MaxBufferSize = 100000}
            config.LocalCacheProperties = New DataCacheLocalCacheProperties(10000, New TimeSpan(0, 5, 0), DataCacheLocalCacheInvalidationPolicy.TimeoutBased)
            Dim myCacheFactory As New DataCacheFactory(config)

            myDefaultCache = myCacheFactory.GetCache("default")
        End Sub

        Private Shared Sub WriteSuccess(ByVal format As String, ByVal ParamArray args() As Object)
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine()
            Console.WriteLine(format, args)
        End Sub

        Private Shared Sub WriteFailure(ByVal format As String, ByVal ParamArray args() As Object)
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine()
            Console.WriteLine(format, args)
        End Sub

        Private Shared Sub TryClearOldKey(ByVal cache As DataCache, ByVal key As String)
            If Nothing IsNot cache.Get(key) Then
                cache.Remove(key)
            End If
        End Sub


    End Class
End Namespace