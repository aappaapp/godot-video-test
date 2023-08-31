extends Button


func _on_pressed():
	Global.is_recording = false
	var a = FileAccess.open("res://output.mp4", FileAccess.WRITE)
	a.store_buffer(FFmpeg.Get())
	a.close()
