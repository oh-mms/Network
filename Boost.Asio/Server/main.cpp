
//  This code is a replica created by studying ndb796's course.
//  Created by oh-mms. 2020.
//  e-mail	: chohmms@gmail.com
//  blog	: https://blog.naver.com/oh-mms
//  gitHub	: https://github.com/oh-mms
//  You can use it freely in any way.
//  If you want to contact me, please send a mail.

// This is a small TCP socket progam using Boost.Asio.

#define _CRT_SECURE_NO_WARNINGS
#include <ctime>
#include <iostream>
#include <string>
#include <boost/asio.hpp>

using boost::asio::ip::tcp;
using namespace std;

// Returns the date and time information of the server computer.
string make_daytime_string()
{
	time_t now = time(0);
	return ctime(&now);
}

int main()
{
	try {
		// Basically, the Boost.Asio program has one IO Service object.
		boost::asio::io_service io_service;

		// Generates a manual socket that receives connections to port 13 of the TCP protocol.
		// Port 13 is the DAYTIME protocol.
		tcp::acceptor acceptor(io_service, tcp::endpoint(tcp::v4(), 13));

		// Repeat indefinitely for all clients.
		while (1) {
			// Create a socket object and wait for the connection.
			tcp::socket socket(io_service);
			acceptor.accept(socket);

			// Generate a message to send to the client when the connection is complete.
			string message = make_daytime_string();

			// Send a message to the client.
			boost::system::error_code ignored_error;
			boost::asio::write(socket, boost::asio::buffer(message), ignored_error);
		}
	}
	catch (exception& e) {
		cerr << e.what() << '\n';
	}

	return 0;
}