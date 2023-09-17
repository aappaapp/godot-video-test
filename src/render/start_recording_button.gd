extends Button


func _on_pressed():
	Encoder.Initialize(Global.ffmpeg_path, 1280, 720)
	Global.is_recording = true
	Global.frame = 0
