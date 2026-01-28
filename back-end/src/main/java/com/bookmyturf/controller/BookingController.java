package com.bookmyturf.controller;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.bookmyturf.dto.BookingRequestDTO;
import com.bookmyturf.dto.BookingResponseDTO;
import com.bookmyturf.dto.CancelBookingRequestDTO;
import com.bookmyturf.service.BookingService;

@RestController
@RequestMapping("/api/bookings")
public class BookingController {

    @Autowired
    private BookingService bookingService;

    //Create Booking
    @PostMapping("/book")
    public BookingResponseDTO createBooking(@RequestBody BookingRequestDTO dto) {
        // Use service to create booking and map to DTO
        return bookingService.mapToResponse(bookingService.createBooking(dto));
    }

    //User Bookings
   
    @PostMapping("/user/bookings")
    public List<BookingResponseDTO> getUserBookings(@RequestBody BookingRequestDTO dto) {
       
        return bookingService.getUserBookings(dto.getUserId())
                             .stream()
                             .map(bookingService::mapToResponse)
                             .collect(Collectors.toList());
    }
    
    @PutMapping("/cancel")
    public ResponseEntity<String> cancelBooking(
            @RequestBody CancelBookingRequestDTO request) {

        if (request.getBookingId() == null) {
            throw new RuntimeException("BookingId must not be null");
        }

        bookingService.cancelBooking(request.getBookingId());
        return ResponseEntity.ok("Booking cancelled successfully");
    }

}
