# Municipality Services Application

## Overview

This is a C# .NET Framework Windows Forms application designed to streamline municipal services in South Africa. The application provides a user-friendly platform for citizens to access and request various municipal services efficiently.

The application allows users to report municipal issues, view local events, and track service requests.

#### Demonstration of the Application: 

## How to Compile:

1. Open Visual Studio.
2. Select 'Open a project or solution'.
3. Navigate to the MunicipalityApplicatiion.sln file and open it.
4. Ensure the configuration is set to 'Debug' and platform to 'Any CPU'.
5. Click 'Build' → 'Build Solution' or press Ctrl + Shift + B.

## How to Run:

1. After compiling, click the green ‘Start/Play’ button in Visual Studio.
2. The main menu will be displayed where you press the option Local Events & Announcements. 
3. The Local Events Form will open showing the Local Events that have been seeded in the Event Repository for the demo. 
3. You can scroll vertically through events and click items to view more details.
4. A search panel is also at the top of the page where they can filter their options by Name, Category & Date, with a search and clear button. 
5. See “recommended for you”events and updates in a separate panel.

## Features

### Main Menu:

	•	Report Issues → Opens a form where users can report municipal service issues.
	•	Local Events & Announcements → View local events and announcements.
	•	Service Request Status → Track the status of service requests.

### Part 1: Report Issues Form

	•	Location Input (TextBox): Users can enter the location of the issue.
	•	Category Selection (ComboBox): Select the type of issue (Sanitation, Roads, Utilities, Safety, Other).
	•	Description (RichTextBox): Provide a detailed description of the issue.
	•	Media Attachment (Button + OpenFileDialog): Attach images or documents to support the issue report.
	•	Submit Button: Finalize the report submission with a confirmation message.
	•	Engagement Feature (Label/ProgressBar): Displays progress and encouraging messages as the user fills in the report form.
	•	Navigation Button (Button): Return back to the main menu form.

### Part 2: Local Events & Announcements

	•	Events Listing (Panel/ListView): Displays upcoming local events and announcements in a visually organized manner.
	•	Search Functionality (TextBox + ComboBox + DatePicker): Filter events by name, category, and date.
	•	Clear/Search Buttons: Reset or perform searches efficiently.
	•	Recommended Events (Panel/ListBox): Suggests events based on user interaction and previous searches.
	•	Efficient Event Storage: Uses Sorted Dictionaries and other data structures to optimise retrieval and filtering.
	•	Responsive Interface: Users can scroll through events and click to view more details.

### Part 3: Service Request Status

	•	Service Requests Grid (DataGridView): Displays all submitted service requests with details including:
	•	Request ID
	•	Title & Description
	•	Priority & Status
	•	Created/Updated Timestamps
	•	Location
	•	Search by ID (TextBox + Button): Quickly locate a request using its unique ID.
	•	Filter by Status (ComboBox): Filter requests based on their current status (Pending, In Progress, Completed).
	•	Show Top Urgent Requests (Button): Highlights the most urgent requests based on priority.
	•	Insights Panel (ListBox): Provides analytics including:
	•	Total number of requests
	•	Oldest and newest requests
	•	Top urgent requests
	•	Connected locations via BFS traversal
	•	Minimum Spanning Tree of areas for quick visualization
	•	Efficient Data Management: The backend uses:
	•	Binary Search Trees for fast ID lookup
	•	AVL Trees for chronological ordering
	•	Red-Black Trees for alphabetical ordering
	•	Priority Queue for urgent requests
	•	Graph Data Structures for location-based insights

## About

#### Project By: 
Emma Jae Dunn | ST10301125

#### Module:
Programming 3B - PROG7312 - POE

#### Youtube Link for Demonstration: 

