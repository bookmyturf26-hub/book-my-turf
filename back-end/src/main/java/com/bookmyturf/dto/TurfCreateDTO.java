

	package com.bookmyturf.dto;

	public class TurfCreateDTO {

	    private Integer turfOwnerId;
	    private String turfName;
	    private String location;
	    private String city;
	    private String description;
	    private String turfStatus;
		public Integer getTurfOwnerId() {
			return turfOwnerId;
		}
		public void setTurfOwnerId(Integer turfOwnerId) {
			this.turfOwnerId = turfOwnerId;
		}
		public String getTurfName() {
			return turfName;
		}
		public void setTurfName(String turfName) {
			this.turfName = turfName;
		}
		public String getLocation() {
			return location;
		}
		public void setLocation(String location) {
			this.location = location;
		}
		public String getCity() {
			return city;
		}
		public void setCity(String city) {
			this.city = city;
		}
		public String getDescription() {
			return description;
		}
		public void setDescription(String description) {
			this.description = description;
		}
		public String getTurfStatus() {
			return turfStatus;
		}
		public void setTurfStatus(String turfStatus) {
			this.turfStatus = turfStatus;
		}
	}


