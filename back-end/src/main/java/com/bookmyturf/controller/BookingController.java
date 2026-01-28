package com.bookmyturf.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.bookmyturf.entity.Booking;
import com.bookmyturf.service.BookingService;

@RestController
	@RequestMapping("/api/bookings")
	public class BookingController {

	    @Autowired
	    private BookingService bookingService;

	    @PostMapping
	    public Booking book(@RequestBody Booking booking) {
	        return bookingService.createBooking(booking);
	    }

	    @GetMapping("/user/{userId}")
	    public List<Booking> getUserBookings(@PathVariable Integer userId) {
	        return bookingService.getUserBookings(userId);
	    }
	}


