package main

import (
	"fmt"
	"net"
	"os"
)

//CheckError 檢查錯誤
func CheckError(err error) {
	if err != nil{
		fmt.Println("Error: ", err)
		os.Exit(0)
	}
}

func main(){
	ServerAddr, err := net.ResolveUDPAddr("udp", ":7938")
	CheckError(err)
	ServerConn, err := net.ListenUDP("udp", ServerAddr)
	CheckError(err)
	defer ServerConn.Close()

	buff := make([]byte, 1024)
	for{
		n, addr, err := ServerConn.ReadFromUDP(buff)
		fmt.Println("收到 ", string(buff[0:n]), " 來自 ", addr)
		if err != nil{
			fmt.Println("Error: ", err)
		}
	}
}