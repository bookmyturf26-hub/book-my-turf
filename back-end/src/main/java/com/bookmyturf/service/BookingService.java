package com.bookmyturf.service;


import java.time.LocalDateTime;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.bookmyturf.dto.BookingRequestDTO;
import com.bookmyturf.dto.BookingResponseDTO;
import com.bookmyturf.entity.Booking;
import com.bookmyturf.entity.SlotMaster;
import com.bookmyturf.entity.User;
import com.bookmyturf.repository.BookingRepository;
import com.bookmyturf.repository.SlotMasterRepository;
import com.bookmyturf.repository.UserRepository;
import com.bookmyturf.enums.*;
import jakarta.transaction.Transactional;

@Service
public class BookingService {

    @Autowired
    private BookingRepository bookingRepository;

    @Autowired
    private SlotMasterRepository slotRepository;

    @Autowired
    private UserRepository userRepository;

    @Transactional
    public Booking createBooking(BookingRequestDTO dto) {

        //  Fetch slot
        SlotMaster slot = slotRepository.findById(dto.getSlotId())
                .orElseThrow(() -> new RuntimeException("Slot not found"));

        if (!slot.getIsAvailable()) {
            throw new RuntimeException("Slot already booked");
        }

        //  Fetch user
        User user = userRepository.findById(dto.getUserId())
                .orElseThrow(() -> new RuntimeException("User not found"));

        //  Create booking
        Booking booking = new Booking();
        booking.setUser(user);
        booking.setSlot(slot);
        booking.setTotalAmount(dto.getTotalAmount());
        booking.setBookingStatus(BookingStatus.Confirmed);
        booking.setPaymentStatus(PaymentStatus.Pending);
        booking.setBookingDate(LocalDateTime.now()); // ✅ Set booking date

        // Mark slot as unavailable
        slot.setIsAvailable(false);
        slotRepository.save(slot);

        // Save booking and return
        return bookingRepository.save(booking);
    }

    // Map entity to DTO for response
    public BookingResponseDTO mapToResponse(Booking booking) {
        BookingResponseDTO response = new BookingResponseDTO();
        response.setBookingId(booking.getBookingId());
        response.setUserId(booking.getUser().getUserID());
        response.setSlotId(booking.getSlot().getSlotId());
        response.setTotalAmount(booking.getTotalAmount());
        response.setBookingStatus(booking.getBookingStatus().name());
        response.setPaymentStatus(booking.getPaymentStatus().name());
        response.setBookingDate(booking.getBookingDate()); // ✅ works with LocalDateTime
        return response;
    }

    // Get all bookings for a user
    public List<Booking> getUserBookings(Integer userId) {
        return bookingRepository.findByUser_UserID(userId);
    }
}

