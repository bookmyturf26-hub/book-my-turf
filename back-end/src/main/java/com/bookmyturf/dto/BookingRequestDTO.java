package com.bookmyturf.dto;

	import java.math.BigDecimal;

	public class BookingRequestDTO {

	    private Integer userId;
	    private Integer slotId;
	    private BigDecimal totalAmount;
		public Integer getUserId() {
			return userId;
		}
		public void setUserId(Integer userId) {
			this.userId = userId;
		}
		public Integer getSlotId() {
			return slotId;
		}
		public void setSlotId(Integer slotId) {
			this.slotId = slotId;
		}
		public BigDecimal getTotalAmount() {
			return totalAmount;
		}
		public void setTotalAmount(BigDecimal totalAmount) {
			this.totalAmount = totalAmount;
		}
	}

	    

	    
	  