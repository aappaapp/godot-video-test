extends Button


func _on_pressed():
	Global.is_recording = false
	FFmpeg.Get()
