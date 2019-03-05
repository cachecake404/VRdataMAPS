#Change this to your code
try:
	f = open("ID.txt","r")
	ID = f.read()
	ID = int(ID)
	f.close()
	#Appply ML Code here
	f = open("RES.txt","w")
	if(ID==123):
		f.write("32.734002\n-97.119409\nTimber Brook\n1") #Latitude\nLongitude\nName\n(0 or 1)
	if(ID==69):
		f.write("44.954445\n-93.091301\nSaint Paul\n0") #Latitude\nLongitude\nName\n(0 or 1)
	if(ID==21):
		f.write("43.723231\n10.396597\nLeaning Tower of Pisa\n0") #Latitude\nLongitude\nName\n(0 or 1)
	if(ID==360):
		f.write("32.985809\n-96.750112\nUTD\n0") #Latitude\nLongitude\nName\n(0 or 1)
	#IF NOT ANY OF THERE (123 orr d69 or 21 or 360) then call API and write data
	else:
		f.write("")
	
	f.close()
except Exception as e:
	print(e)

#32.734002
#-97.119409
#Timber Brook
#1

