package main

import (
	"bufio"
	"net"
)

func main(){
	listener, _ := net.Listen("tcp", ":7938")
	println("Server starting...")
	
	for{
		conn, _ := listener.Accept()
		go ClientLogit(conn)
	}
}

func ClientLogit(conn net.Conn){
	s, _ := bufio.NewReader(conn).ReadString('\n')
	println("Sent from client:",s)
	conn.Write([]byte("yolo\n"))
	conn.Close()
}