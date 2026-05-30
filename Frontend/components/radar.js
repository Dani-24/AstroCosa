import { useEffect } from "react";
import { View, StyleSheet } from "react-native";

export default function Radar() {
    useEffect(() => {
        const canvas = document.getElementById("radar");
        const ctx = canvas.getContext("2d");

        const cx = canvas.width / 2;
        const cy = canvas.height / 2;

        const missions = [
            { x: 80, y: 60, name: "Mission 1", alpha: 0 },
            { x: 200, y: 120, name: "Mission 2", alpha: 0 },
            { x: 140, y: 220, name: "Mission 3", alpha: 0 },
            { x: 140, y: 100, name: "Boss", isBoss: true, alpha: 0 },
        ];

        let angle = 0;
        let bossAngle = 0;

        function getAngle(x, y) {
            return Math.atan2(y - cy, x - cx);
        }

        function normalize(a) {
            return (a + Math.PI * 2) % (Math.PI * 2);
        }

        function draw() {
            const w = canvas.width;
            const h = canvas.height;
            const cx = w / 2;
            const cy = h / 2;

            ctx.clearRect(0, 0, w, h);

            // background
            ctx.fillStyle = "#001000";
            ctx.fillRect(0, 0, w, h);

            // radar circles
            ctx.strokeStyle = "#00ff66";
            ctx.lineWidth = 1;

            for (let r = 50; r <= 150; r += 50) {
                ctx.beginPath();
                ctx.arc(cx, cy, r, 0, Math.PI * 2);
                ctx.stroke();
            }

            ctx.beginPath();
            ctx.moveTo(cx, 0);
            ctx.lineTo(cx, h);
            ctx.moveTo(0, cy);
            ctx.lineTo(w, cy);
            ctx.stroke();

            const sweepAngle = normalize(angle);
            const gradient = ctx.createRadialGradient(cx, cy, 0, cx, cy, 180);
            gradient.addColorStop(0, "rgba(0,255,0,0.35)");
            gradient.addColorStop(1, "rgba(0,255,0,0)");

            ctx.save();
            ctx.translate(cx, cy);
            ctx.rotate(angle);

            ctx.beginPath();
            ctx.moveTo(0, 0);
            ctx.arc(0, 0, 180, -0.12, 0.12);
            ctx.closePath();

            ctx.fillStyle = gradient;
            ctx.fill();

            ctx.restore();

            const boss = missions.find(m => m.isBoss);

            if (boss) {
                bossAngle -= 0.002;
                const radius = 115;

                boss.x = cx + Math.cos(bossAngle) * radius;
                boss.y = cy + Math.sin(bossAngle) * radius;
            }

            missions.forEach((m) => {
                const missionAngle = normalize(getAngle(m.x, m.y));
                const diff = Math.abs(sweepAngle - missionAngle);

                const detected = diff < 0.15;

                if (detected) {
                    m.alpha = 1;
                } else {
                    m.alpha *= 0.982;
                }

                if (m.alpha > 0.01) {
                    ctx.beginPath();
                    ctx.arc(m.x, m.y, m.isBoss ? 10 : 6, 0, Math.PI * 2);

                    ctx.fillStyle = m.isBoss
                        ? `rgba(255,0,0,${m.alpha})`
                        : `rgba(0,255,0,${m.alpha})`;

                    ctx.fill();

                    ctx.fillStyle = `rgba(255,255,255,${m.alpha})`;
                    ctx.fillText(m.name, m.x + 10, m.y);
                }
            });

            angle += 0.02;

            requestAnimationFrame(draw);
        }

        draw();
    }, []);

    return (
        <View style={styles.container}>
            <canvas
                id="radar"
                width="300"
                height="300"
                style={styles.radar}
            />
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    radar: {
        border: "2px solid #00ff66",
        borderRadius: "50%",
    }
})