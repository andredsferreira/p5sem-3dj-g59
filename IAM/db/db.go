package db

import (
	"database/sql"
	"fmt"
	"log"

	"github.com/go-sql-driver/mysql"
)

var MySql *sql.DB

func ConnectDatabase() {
	cfg := mysql.Config{
		User:   "root",
		Passwd: "42DctTV7IC0F",
		Net:    "tcp",
		Addr:   "vsgate-s1.dei.isep.ipp.pt:10694",
		DBName: "projeto5sem_iam",
	}
	var err error
	MySql, err = sql.Open("mysql", cfg.FormatDSN())
	if err != nil {
		log.Fatal(err)
	}
	pingErr := MySql.Ping()
	if pingErr != nil {
		log.Fatal(pingErr)
	}
	fmt.Println("database connected")
}
