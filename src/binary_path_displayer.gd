extends Label


func _process(_delta) -> void:
	text = "Binary path: %s" % ("null" if Global.ffmpeg_path == "" else Global.ffmpeg_path)
