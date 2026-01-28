package com.bookmyturf.repository;

	import com.bookmyturf.entity.Booking;
	import com.bookmyturf.enums.BookingStatus;
	import org.springframework.data.jpa.repository.JpaRepository;

	import java.util.List;

	public interface BookingRepository extends JpaRepository<Booking, Integer> {

	    // All bookings of a user
	    List<Booking> findByUser_UserID(Integer userId);

	    // Filter bookings by status
	    List<Booking> findByBookingStatus(BookingStatus bookingStatus);

	    // User bookings by status
	    List<Booking> findByUser_UserIDAndBookingStatus(
	            Integer userId,
	            BookingStatus bookingStatus
	    );
	}


