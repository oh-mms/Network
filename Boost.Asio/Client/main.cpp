
//  This code is a replica created by studying ndb796's course.
//  Created by oh-mms. 2020.
//  e-mail	: chohmms@gmail.com
//  blog	: https://blog.naver.com/oh-mms
//  gitHub	: https://github.com/oh-mms
//  You can use it freely in any way.
//  If you want to contact me, please send a mail.

// This is a small TCP socket progam using Boost.Asio.

#include <iostream>
#include <boost/array.hpp>
#include <boost/asio.hpp>

using boost::asio::ip::tcp;
using namespace std;

int main() {
	try {
		// Basically, the Boost.Asio program has one IO Service object.
		boost::asio::io_service io_service;

		// Use Resolver to rename a domain to a TCP endpoint.
		tcp::resolver resolver(io_service);

		// Local servers are written as servers, and services are written as Daytime protocols. In this example, 127.0.0.1.
		tcp::resolver::query query("localhost", "daytime");

		// Obtain IP addresses and port numbers via DNS.
		tcp::resolver::iterator endpoint_iterator = resolver.resolve(query);

		// Initialize the socket object and connect it to the server.
		tcp::socket socket(io_service);
		boost::asio::connect(socket, endpoint_iterator);

		while (1) {
			// Declares buffers and error-handling variables.
			boost::array<char, 128> buf;
			boost::system::error_code error;

			// Get data from server using buffer.
			size_t len = socket.read_some(boost::asio::buffer(buf), error);
			if (error == boost::asio::error::eof)
				break;
			else if (error)
				throw boost::system::system_error(error);

			// The data contained in the buffer is printed on the screen.
			cout.write(buf.data(), len);
		}
	}
	catch (exception& e) {
		cout << e.what() << endl;
	}

	system("pause");
	return 0;
}